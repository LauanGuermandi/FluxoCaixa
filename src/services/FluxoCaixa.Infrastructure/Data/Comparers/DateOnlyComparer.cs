using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FluxoCaixa.Infrastructure.Data.Comparers;
public class DateOnlyComparer : ValueComparer<DateOnly>
{
	public DateOnlyComparer() : base(
		(d1, d2) => d1.DayNumber == d2.DayNumber,
		d => d.GetHashCode())
	{
	}
}
