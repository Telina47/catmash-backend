using AutoMapper;
using Catmash.Application.DTOs;
using Catmash.Application.Services;
using Catmash.Domain;
using Catmash.Domain.Interfaces;
using Catmash.Infrastructure.Mapping;
using FluentAssertions;
using Moq;

namespace Catmash.Tests.Services;

public class UserStatsServiceTests
{
    private readonly Mock<IVoteRepository> _voteRepo = new();
    private readonly Mock<ICatRepository> _catRepo = new();
    private readonly IMapper _mapper;

    public UserStatsServiceTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetComparisonAsync_ShouldReturnValidStats()
    {
        // Arrange
        var voterId = "user123";

        var cats = Enumerable.Range(1, 10).Select(i => new Cat
        {
            Id = $"cat{i}",
            ImageUrl = $"http://url/cat{i}.jpg",
            Score = 1000 + i,
            Wins = i,
            Losses = 10 - i
        }).ToList();

        var votes = new List<Vote>
        {
            new() { VoterId = voterId, WinnerCatId = "cat1", LoserCatId = "cat2" },
            new() { VoterId = voterId, WinnerCatId = "cat3", LoserCatId = "cat4" }
        };

        _catRepo.Setup(r => r.GetTopCatsAsync(100)).ReturnsAsync(cats);
        _voteRepo.Setup(r => r.GetVotesByUserAsync(voterId)).ReturnsAsync(votes);

        var service = new UserStatsService(_voteRepo.Object, _catRepo.Object, _mapper);

        // Act
        var result = await service.GetComparisonAsync(voterId, TopCount.Top5);

        // Assert
        result.TopCount.Should().Be(TopCount.Top5);
        result.TopGlobal.Should().HaveCount(5);
        result.TopUser.Should().OnlyContain(c => c.Id == "cat1" || c.Id == "cat2" || c.Id == "cat3" || c.Id == "cat4");
        result.AgreementRate.Should().BeGreaterThanOrEqualTo(0);
    }
}
