using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Features.Reviews.Commands.UpdateReview
{
	public class UpdateReviewCommandValidator:AbstractValidator<UpdateReviewCommand>
	{
		public UpdateReviewCommandValidator()
		{
			RuleFor(x => x.Id).NotNull().GreaterThan(0);
			RuleFor(x => x.Comment).NotNull().Length(3, 600);
			RuleFor(x => x.Stars).NotNull().GreaterThan(0).LessThanOrEqualTo(5);
		}
	}
}
