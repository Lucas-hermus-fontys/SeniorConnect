using Domain.Analysis;
using Domain.Enum;
using Domain.Model;
using Domain.Service;
using FluentAssertions;
using Test.Mocks;
using Test.Stubs;
using Test.Util;
using Web.Models;

namespace Test.Service;

public class DiscussionFormServiceTest
{
    private readonly DiscussionFormService _discussionFormService;
    private readonly DiscussionFormRepositoryMock _discussionFormMock;

    public DiscussionFormServiceTest()
    {
        _discussionFormMock = new DiscussionFormRepositoryMock();
        _discussionFormService = new DiscussionFormService(_discussionFormMock, new TestDiscussionFormValidator(), new KeywordMatchingStrategy());
    }

    [Fact]
    public void CreateDiscussionFrom_Should_Call_AssignTopicsToDiscussionForm()
    {
        var user = new User(); 
        var request = new DiscussionFormCreateRequest(); 
        
        _discussionFormService.CreateDiscussionFrom(user, request);
        
        _discussionFormMock.AssignTopicsCalled.Should().BeTrue("The topics should have been assigned to the discussion form.");
    }
    
    [Fact]
    public void GetDiscussionFormById_Should_Return_Correct_DiscussionForm()
    {
        int discussionFormId = 1;  
        var expectedDiscussionForm = new CollaborativeSpace
        {
            Type = CollaborativeSpaceType.CHAT
        };  
        var discussionFormRepositoryMock = new DiscussionFormRepositoryMock();
        var discussionFormService = new DiscussionFormService(discussionFormRepositoryMock, new TestDiscussionFormValidator(), new KeywordMatchingStrategy());

        var result = discussionFormService.GetDiscussionFormById(discussionFormId);

        result.Should().BeEquivalentTo(expectedDiscussionForm, options => options.ExcludingMissingMembers());
    }
    
}

