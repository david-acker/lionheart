using Lionheart.Application;
using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Trades.Enums;
using Moq;
using Xunit;

namespace Lionheart.Pipeline.Extraction;

public class TradeExtractionEngineTests
{
    #region Scaffolding
    internal class MockTrade : IBaseTrade
    {
        public int BaseTradeId { get; set; }
        public int? UserId { get; set; }
        public int OrderTypeId { get; set; }
        public DateTime ExecutedAt { get; set; }
        public decimal Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal Notional { get; set; }
        public OrderType OrderType { get; set; }
        public TransactionType TransactionType { get; set; }
    }

    private static Mock<ITradeExtractor<MockTrade>> GetMockTradeExtractor(bool isSuccessful)
    {
        var mockExtractor = new Mock<ITradeExtractor<MockTrade>>();

        MockTrade? extractedTrade = isSuccessful ? new MockTrade() : null;

        mockExtractor.Setup(x => x.Extract(It.IsAny<EmailInputDomainModel>()))
            .Returns(extractedTrade);

        return mockExtractor;
    }
    #endregion Scaffolding

    [Fact]
    public void Extract_HasMatchingExtractor_ShouldReturnExtractedTrade()
    {
        // Arrange
        var email = new EmailInputDomainModel
        {
            SourceEmailId = "email-id",
            Body = "email-body"
        };

        Mock<ITradeExtractor<MockTrade>> unsuccessfulMockExtractor = GetMockTradeExtractor(false);
        Mock<ITradeExtractor<MockTrade>> successfulMockExtractor = GetMockTradeExtractor(true);
        Mock<ITradeExtractor<MockTrade>> skippedMockExtractor = GetMockTradeExtractor(false);

        var mockExtractors = new Mock<ITradeExtractor<MockTrade>>[]
        {
            unsuccessfulMockExtractor,
            successfulMockExtractor,
            skippedMockExtractor
        };

        var mockLogger = new Mock<ILionheartLogger>();

        var engine = new TradeExtractionEngine<MockTrade>(
            mockLogger.Object,
            mockExtractors.Select(x => x.Object).ToList());

        // Act
        MockTrade? result = engine.Extract(email);

        // Assert
        unsuccessfulMockExtractor
            .Verify(x => x.Extract(
                It.Is<EmailInputDomainModel>(y =>
                    y.SourceEmailId == email.SourceEmailId &&
                    y.Body == email.Body)), 
                Times.Once);

        successfulMockExtractor
           .Verify(x => x.Extract(
               It.Is<EmailInputDomainModel>(y =>
                   y.SourceEmailId == email.SourceEmailId &&
                   y.Body == email.Body)),
               Times.Once);

        skippedMockExtractor
           .Verify(x => x.Extract(It.IsAny<EmailInputDomainModel>()), Times.Never);

        Assert.NotNull(result);
    }

    [Fact]
    public void Extract_HasNoMatchingExtractor_ShouldReturnNull()
    {
        // Arrange
        var email = new EmailInputDomainModel
        {
            SourceEmailId = "email-id",
            Body = "email-body"
        };

        Mock<ITradeExtractor<MockTrade>> unsuccessfulMockExtractor = GetMockTradeExtractor(false);
        Mock<ITradeExtractor<MockTrade>> successfulMockExtractor = GetMockTradeExtractor(true);
        Mock<ITradeExtractor<MockTrade>> skippedMockExtractor = GetMockTradeExtractor(false);

        IEnumerable<Mock<ITradeExtractor<MockTrade>>> mockExtractors =
            Enumerable.Range(1, 3).Select(x => GetMockTradeExtractor(false)).ToList();

        var mockLogger = new Mock<ILionheartLogger>();

        var engine = new TradeExtractionEngine<MockTrade>(
            mockLogger.Object,
            mockExtractors.Select(x => x.Object).ToList());

        // Act
        MockTrade? result = engine.Extract(email);

        // Assert
        foreach (var mockExtractor in mockExtractors)
        {
            mockExtractor
                .Verify(x => x.Extract(
                   It.Is<EmailInputDomainModel>(y =>
                       y.SourceEmailId == email.SourceEmailId &&
                       y.Body == email.Body)),
                   Times.Once);
        }

        var expectedMessage = $"No matching extractors for email {email.SourceEmailId}.";
        mockLogger.Verify(x => x.LogWarning(It.Is<string>(y => y == expectedMessage)), Times.Once);

        Assert.Null(result);
    }
}
