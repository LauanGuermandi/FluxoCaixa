using Microsoft.AspNetCore.Identity;

namespace Identidade;

public class MessagesExtension : IdentityErrorDescriber
{
	public override IdentityError DefaultError()
		=> new() { Code = nameof(DefaultError), Description = $"Ocorreu um erro desconhecido." };

	public override IdentityError ConcurrencyFailure()
		=> new() { Code = nameof(ConcurrencyFailure), Description = "Falha de concorrência otimista, o objeto foi modificado." };

	public override IdentityError PasswordMismatch()
		=> new() { Code = nameof(PasswordMismatch), Description = "Senha incorreta." };

	public override IdentityError InvalidToken()
		=> new() { Code = nameof(InvalidToken), Description = "Token inválido." };

	public override IdentityError LoginAlreadyAssociated()
		=> new() { Code = nameof(LoginAlreadyAssociated), Description = "Já existe um usuário com este login." };

	public override IdentityError InvalidUserName(string userName)
		=> new() { Code = nameof(InvalidUserName), Description = $"O login '{userName}' é inválido, pode conter apenas letras ou dígitos." };

	public override IdentityError InvalidEmail(string email)
		=> new() { Code = nameof(InvalidEmail), Description = $"O email '{email}' é inválido." };

	public override IdentityError DuplicateUserName(string userName)
		=> new() { Code = nameof(DuplicateUserName), Description = $"O login '{userName}' já está sendo utilizado." };

	public override IdentityError DuplicateEmail(string email)
		=> new() { Code = nameof(DuplicateEmail), Description = $"O email '{email}' já está sendo utilizado." };

	public override IdentityError InvalidRoleName(string role)
		=> new() { Code = nameof(InvalidRoleName), Description = $"A permissão '{role}' é inválida." };

	public override IdentityError DuplicateRoleName(string role)
		=> new() { Code = nameof(DuplicateRoleName), Description = $"A permissão '{role}' já está sendo utilizada." };

	public override IdentityError UserAlreadyHasPassword()
		=> new() { Code = nameof(UserAlreadyHasPassword), Description = "O usuário já possui uma senha definida." };

	public override IdentityError UserLockoutNotEnabled()
		=> new() { Code = nameof(UserLockoutNotEnabled), Description = "O lockout não está habilitado para este usuário." };

	public override IdentityError UserAlreadyInRole(string role)
		=> new() { Code = nameof(UserAlreadyInRole), Description = $"O usuário já possui a permissão '{role}'." };

	public override IdentityError UserNotInRole(string role)
		=> new() { Code = nameof(UserNotInRole), Description = $"O usuário não tem a permissão '{role}'." };

	public override IdentityError PasswordTooShort(int length)
		=> new() { Code = nameof(PasswordTooShort), Description = $"As senhas devem conter ao menos {length} caracteres." };

	public override IdentityError PasswordRequiresNonAlphanumeric()
		=> new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "As senhas devem conter ao menos um caracter não alfanumérico." };

	public override IdentityError PasswordRequiresDigit()
		=> new() { Code = nameof(PasswordRequiresDigit), Description = "As senhas devem conter ao menos um digito ('0'-'9')." };

	public override IdentityError PasswordRequiresLower()
		=> new() { Code = nameof(PasswordRequiresLower), Description = "As senhas devem conter ao menos um caracter em caixa baixa ('a'-'z')." };

	public override IdentityError PasswordRequiresUpper()
		=> new() { Code = nameof(PasswordRequiresUpper), Description = "As senhas devem conter ao menos um caracter em caixa alta ('A'-'Z')." };
}
