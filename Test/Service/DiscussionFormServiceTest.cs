using Domain.Model;
using Domain.Service;
using Web.Models;
using Domain.Analysis;
using Domain.Validation;
using FluentAssertions;
using Test.Mocks;

namespace Test.Service;

public class DiscussionFormServiceTest
{
    private readonly DiscussionFormService _discussionFormService;
    private readonly DiscussionFormRepositoryMock _discussionFormMock;

    public DiscussionFormServiceTest()
    {
        _discussionFormMock = new DiscussionFormRepositoryMock();
        _discussionFormService = new DiscussionFormService(_discussionFormMock, new DiscussionFormValidator(), new KeywordMatchingStrategy());
    }

    [Fact]
    public void CreateDiscussionFrom_Should_Call_AssignTopicsToDiscussionForm()
    {
        var user = new User(); 
        var request = new DiscussionFormCreateRequest(); 
        
        _discussionFormService.CreateDiscussionFrom(user, request);
        
        _discussionFormMock.AssignTopicsCalled.Should().BeTrue("The topics should have been assigned to the discussion form.");
    }
}

