using Domain.Analysis;
using Domain.Interface;
using Domain.Model;
using Microsoft.VisualBasic.CompilerServices;
using Web.Models;

namespace Domain.Service;

public class DiscussionFormService
{
    private readonly IDiscussionFormRepository _discussionFormRepository;

    public DiscussionFormService(IDiscussionFormRepository discussionFormRepository, IGroupChatRepository groupchatRepository)
    {
        _discussionFormRepository = discussionFormRepository;
    }

    public List<CollaborativeSpace> GetDiscussionForms()
    {
        List<CollaborativeSpace> discussionForms = _discussionFormRepository.GetDiscussionForms();
        HydrateDiscussionForms(discussionForms);
        return discussionForms;
    }
    
    public CollaborativeSpace GetDiscussionFormById(int id)
    {
        CollaborativeSpace discussionForm = _discussionFormRepository.GetById(id);
        HydrateDiscussionForms(discussionForm);
        return discussionForm;
    }
    
    public void CreateDiscussionFrom(User user, DiscussionFormCreateRequest request)
    {
        int newDiscussionFormId = _discussionFormRepository.CreateDiscussionForm(user, request);
        CollaborativeSpace discussionForm = GetDiscussionFormById(newDiscussionFormId);
        List<Topic> topics = GetTopics();
        DiscussionFormAnalyzer analyzer = new DiscussionFormAnalyzer(discussionForm, topics);
        List<Topic> relatedTopics = analyzer.GetTopicsFromContext();
        if (relatedTopics.Any())
        {
            AddTopicsToDiscussionForm(discussionForm, relatedTopics);
        }
    }

    public void AddTopicsToDiscussionForm(CollaborativeSpace discussionForm, List<Topic> topics)
    {
        _discussionFormRepository.AssignTopicsToDiscussionForm(discussionForm.Id, topics.Select(topic => topic.Id).ToList());
    }

    public List<Topic> GetTopics()
    {
        List<Topic> topics = _discussionFormRepository.GetTopics();
        List<TopicKeyword> topicKeywords = _discussionFormRepository.GetKeywords();
        topics.ForEach(topic => topic.Keywords = topicKeywords.Where(keyword => keyword.TopicId == topic.Id).ToList());
        return topics;
    }
    
    private void HydrateDiscussionForms(CollaborativeSpace discussionForm)
    {
        HydrateDiscussionForms(new List<CollaborativeSpace> { discussionForm });
    }

    private void HydrateDiscussionForms(List<CollaborativeSpace> discussionForms)
    {
        List<User> creators = _discussionFormRepository.GetCreatorsByDiscussionFormIds(discussionForms.Select(g => g.Id).ToList());
        List<Topic> topics = _discussionFormRepository.GetTopicsByCollaborativeSpaceIds(discussionForms.Select(g => g.Id).ToList());
        List<CollaborativeSpaceMessage> comments = _discussionFormRepository.GetCommentsByDiscussionFormIds(discussionForms.Select(g => g.Id).ToList());
        List<User> commentUsers = _discussionFormRepository.GetUsersByCommentIds(comments.Select(g => g.Id).ToList());

        comments.ForEach(g => g.User = commentUsers.FirstOrDefault(c => c.CollaborativeSpaceMessageId == g.Id));
        discussionForms.ForEach(g => g.Creator = creators.FirstOrDefault(c => c.CollaborativeSpaceId == g.Id));
        discussionForms.ForEach(g => g.Topics = topics.Where(c => c.CollaborativeSpaceId == g.Id).ToList());

        OrganizeMessagesIntoGroups(comments, discussionForms);
    }

    private void OrganizeMessagesIntoGroups(List<CollaborativeSpaceMessage> comments, List<CollaborativeSpace> groupChats)
    {
        Dictionary<int, List<CollaborativeSpaceMessage>> groupedComments = comments
            .GroupBy(c => c.ParentId)
            .ToDictionary(g => g.Key, g => g.ToList());

        groupChats.ForEach(gc =>
        {
            gc.CollaborativeSpaceMessages = groupedComments
                .GetValueOrDefault(0)?
                .Where(c => c.CollaborativeSpaceId == gc.Id)
                .ToList() ?? new List<CollaborativeSpaceMessage>();

            gc.CollaborativeSpaceMessages.ForEach(message => { AddChildrenRecursive(message, groupedComments); });
        });
    }

    private void AddChildrenRecursive(CollaborativeSpaceMessage parentMessage,
        Dictionary<int, List<CollaborativeSpaceMessage>> groupedComments)
    {
        if (groupedComments.TryGetValue(parentMessage.Id, out var childMessages))
        {
            parentMessage.ChildMessages = childMessages;

            foreach (CollaborativeSpaceMessage childMessage in childMessages)
            {
                AddChildrenRecursive(childMessage, groupedComments);
            }
        }
    }
}