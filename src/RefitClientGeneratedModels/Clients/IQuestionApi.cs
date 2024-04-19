using Refit;
using RefitClientGeneratedModels.Authorization;
using RefitClientTemplate.GeneratedModels;

namespace RefitClientGeneratedModels.Clients;

[Headers("Content-Type", "application/json")]
[ActAs(Roles.User)]
public interface IQuestionApi
{
    [Get("/api/services/app/Question/GetQuestions")]
    Task<QuestionDtoPagedResultDto> GetQuestionsAsync([Query] int maxResultCount = 20, [Query] int skipCount = 0,
        [Query] string? sorting = null);

    [Post("/api/services/app/Question/CreateQuestion")]
    Task CreateQuestionAsync([Body] CreateQuestionInput input);

    [Get("/api/services/app/Question/GetQuestion")]
    Task<GetQuestionOutput> GetQuestionAsync([Query] bool incrementViewCount, [Query] int id);

    [Post("/api/services/app/Question/VoteUp")]
    Task<VoteChangeOutput> VoteUpAsync([Body] EntityDto input);

    [Post("/api/services/app/Question/VoteDown")]
    Task<VoteChangeOutput> VoteDownAsync([Body] EntityDto input);

    [Post("/api/services/app/Question/SubmitAnswer")]
    Task<SubmitAnswerOutput> SubmitAnswerAsync([Body] SubmitAnswerInput input);

    [Post("/api/services/app/Question/AcceptAnswer")]
    Task AcceptAnswerAsync([Body] EntityDto input);
}
