using ExampleBackend.AppDbContext;
using ExampleBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;
    private readonly ApplicationContext _context;

    public ApiController(ILogger<ApiController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("add-marks")]
    public async Task<IActionResult> AddMarkAsync(AddMarkRequest request)
    {
        var validationResult = ValidateRequest(request);
        if (!validationResult.IsSuccess)
            return new JsonResult(new { IsSuccess = false, Errors = validationResult.Errors });

        await SaveRequestAsync(request);

        return new JsonResult(new { isSuccess = true });
        
    }

    private AddMarksRequestValidationResult ValidateRequest(AddMarkRequest request)
    {
        var isValid = true;
        var errors = new List<string>();

        if(string.IsNullOrEmpty(request.FirstName?.Trim()))
        {
            isValid = false;
            errors.Add("First name is empty");
        }

        if (string.IsNullOrEmpty(request.LastName?.Trim()))
        {
            isValid = false;
            errors.Add("First name is empty");
        }

        if(request.Marks.Count() == 0)
        {
            isValid = false;
            errors.Add("There is no marks");
        }

        foreach (var mark in request.Marks)
        {
            if (string.IsNullOrEmpty(mark.DisciplineName?.Trim()))
            {
                isValid = false;
                errors.Add("Discipline name is empty");
            }

            if(mark.Score< 2 || mark.Score> 5)
            {
                isValid = false;
                errors.Add("Mark should be between 2 and 5");
            }
        }

        return new AddMarksRequestValidationResult() { IsSuccess = isValid, Errors = errors };
    }

    private async Task SaveRequestAsync(AddMarkRequest request)
    {
        var user = await _context.Users.Where(x => x.FirstName == request.FirstName
                                                && x.LastName == request.LastName
                                                && x.MiddleName == request.MiddleName )
                                        .FirstOrDefaultAsync();

        if(user is null)
        {
            user = new Entities.UserEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName
                
            };
            await _context.Users.AddAsync(user);
        }

        foreach(var mark in request.Marks)
        {
            var markEntity = new Entities.MarkEntity()
            {
                DisciplineName = mark.DisciplineName,
                Mark = mark.Score,
            };

            await _context.Marks.AddAsync(markEntity);

        }

        await _context.SaveChangesAsync();
    }
}

