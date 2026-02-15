using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.StudySessions.GetAllSessionYears;

public class GetAllSessionYears
{
    private readonly IStudySessionRepository _studyRepo;

    public GetAllSessionYears(IStudySessionRepository studyRepo)
    {
        _studyRepo = studyRepo;
    }

    public Result<int[]> Handle()
    {
        var sessionYears = _studyRepo.GetAllSessionYears();

        if (sessionYears == null || sessionYears.Count() == 0) return Result<int[]>.Failure(Errors.NoStudySessions);

        return Result<int[]>.Success(sessionYears);
    }
}
