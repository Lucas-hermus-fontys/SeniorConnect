using Domain.Interface;
using Domain.Model;
using Web.Models;

namespace Domain.Service;

public class DiscussionFormService
{
    private readonly IDiscussionFormRepository _discussionFormRepository;

    public DiscussionFormService(IDiscussionFormRepository discussionFormRepository,
        IGroupChatRepository groupchatRepository)
    {
        _discussionFormRepository = discussionFormRepository;
    }

    public List<CollaborativeSpace> GetDiscussionForms()
    {
        List<CollaborativeSpace> discussionForms = _discussionFormRepository.GetDiscussionForms();
        List<User> creators =
            _discussionFormRepository.GetCreatorsByGroupChatIds(discussionForms.Select(g => g.Id).ToList());
        List<CollaborativeSpaceMessage> comments = _discussionFormRepository.GetComments();
        List<User> commentUsers = _discussionFormRepository.GetUserByCommentIds(comments.Select(g => g.Id).ToList());

        comments.ForEach(g => g.User = commentUsers.FirstOrDefault(c => c.CollaborativeSpaceMessageId == g.Id));
        discussionForms.ForEach(g => g.Creator = creators.FirstOrDefault(c => c.CollaborativeSpaceId == g.Id));

        OrganizeMessagesIntoGroups(comments, discussionForms);

        return discussionForms;
    }

    public void CreateDiscussionFrom(User user, DiscussionFormCreateRequest request)
    {
        _discussionFormRepository.CreateDiscussionForm(user, request);
    }
    
    private void OrganizeMessagesIntoGroups(List<CollaborativeSpaceMessage> comments,
        List<CollaborativeSpace> groupChats)
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