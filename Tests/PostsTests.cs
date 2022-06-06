using AutoMapper;

using Bogus;

using Crapper.DTOs.Post;
using Crapper.Features.PostsFeatures.Queries.GetAllPosts;
using Crapper.Features.PostsFeatures.Queries.GetPostById;
using Crapper.Interfaces;
using Crapper.Models;
using Crapper.Profiles;

using Moq;

using Shouldly;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Xunit;

namespace Tests;

public class PostsTests
{
    [Fact]
    public async void GetAllPostsQuery_Default_ReturnsPostDtos()
    {
        int count = 25;

        var testUsers = new Faker<User>()
            .RuleFor(u => u.Id, f => f.IndexFaker)
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Bio, f => f.Lorem.Sentences(f.Random.Number(2, 5)))
            .RuleFor(u => u.Password, f => f.Internet.Password());
        var users = testUsers.Generate(5);

        var testPosts = new Faker<Post>()
            .RuleFor(p => p.Id, f => f.IndexFaker)
            .RuleFor(p => p.Content, f => f.Lorem.Sentences(f.Random.Number(2, 5)))
            .RuleFor(p => p.AuthorId, f => f.PickRandom(users).Id);
        var posts = testPosts.Generate(count);

        var repository = new Mock<IRepository<Post>>();
        repository.Setup(x => x.GetAll()).Returns(posts.AsQueryable());

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PostProfile());
        });
        var mapper = mockMapper.CreateMapper();

        var handler = new GetAllPostsQueryHandler(repository.Object, mapper);

        var result = await handler.Handle(new GetAllPostsQuery(), CancellationToken.None);

        result.ShouldNotBeNull();
        result.Count.ShouldBe(count);
    }

    [Fact]
    public async void GetPostByIdQuery_WithExistingId_ReturnsPostDto()
    {
        int count = 1;

        var testPosts = new Faker<Post>()
            .RuleFor(p => p.Id, f => f.IndexFaker)
            .RuleFor(p => p.Content, f => f.Lorem.Sentences(f.Random.Number(2, 5)));
        var posts = testPosts.Generate(count);

        var repository = new Mock<IRepository<Post>>();
        repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(posts.First());

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PostProfile());
        });
        var mapper = mockMapper.CreateMapper();

        var handler = new GetPostByIdQueryHandler(repository.Object, mapper);
        var result = await handler.Handle(new GetPostByIdQuery(123), CancellationToken.None);

        result.ShouldBeEquivalentTo(mapper.Map<PostDto>(posts.First()));
        result.ShouldNotBeNull();
        result.ShouldBeOfType<PostDto>();
    }

    [Fact]
    public async void GetPostByIdQuery_WithUnexistingId_ReturnsNull()
    {
        var repository = new Mock<IRepository<Post>>();
        repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Post)null);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PostProfile());
        });
        var mapper = mockMapper.CreateMapper();

        var handler = new GetPostByIdQueryHandler(repository.Object, mapper);
        var result = await handler.Handle(new GetPostByIdQuery(123), CancellationToken.None);

        result.ShouldBeNull();
    }
}