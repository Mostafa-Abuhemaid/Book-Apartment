using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Features.Reviews.Commands.AddReview
{
	public class AddReviewCommandValidator:AbstractValidator<AddReviewCommand>
	{
		public AddReviewCommandValidator()
		{
			RuleFor(x => x.PropertyId).NotNull().GreaterThan(0);
			RuleFor(x => x.Review).NotNull().MinimumLength(3).MaximumLength(600);
			RuleFor(x => x.Stars).NotNull().InclusiveBetween(1, 5);
		}
	}
}
