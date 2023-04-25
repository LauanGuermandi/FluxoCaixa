using System.Security.Claims;
using FluentValidation.Results;
using FluxoCaixa.Core.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FluxoCaixa.Core.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public abstract class MainController : Controller
{
	protected ICollection<string> Errors = new List<string>();

	protected string GetAuthenticatedUserEmail()
	{
		var identity = User.Identity as ClaimsIdentity;
		var email = identity?.FindFirst(ClaimTypes.Email)?.Value.ToString();
		return !string.IsNullOrEmpty(email) ? email : string.Empty;
	}

	protected ActionResult CustomResponse(object? result = null)
	{
		if (ValidOperation())
		{
			return Ok(result);
		}

		return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
		{
			{ "Messages", Errors.ToArray() }
		}));
	}

	protected ActionResult CustomResponse(ModelStateDictionary modelState)
	{
		var errors = modelState.Values.SelectMany(e => e.Errors);
		foreach (var error in errors)
		{
			AddErrorToStack(error.ErrorMessage);
		}

		return CustomResponse();
	}

	protected ActionResult CustomResponse(ValidationResult validationResult)
	{
		foreach (var error in validationResult.Errors)
		{
			AddErrorToStack(error.ErrorMessage);
		}

		return CustomResponse();
	}

	protected ActionResult CustomResponse(ResponseResult responseResult)
	{
		ResponseHasErrors(responseResult);

		return CustomResponse();
	}

	protected bool ResponseHasErrors(ResponseResult? responseResult)
	{
		if (responseResult is null || !responseResult.Errors.Messages.Any())
			return false;

		foreach (var errorMessage in responseResult.Errors.Messages)
		{
			AddErrorToStack(errorMessage);
		}

		return true;
	}

	protected bool ValidOperation()
		=> !Errors.Any();

	protected void AddErrorToStack(string error)
		=> Errors.Add(error);

	protected void CleanErrors()
		=> Errors.Clear();
}
