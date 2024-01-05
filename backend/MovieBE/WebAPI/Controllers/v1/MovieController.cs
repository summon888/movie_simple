using Application.Interfaces;
using Application.Services;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class MovieController : ApiController
    {
        private readonly IMovieAppService _movieAppService;

        public MovieController(
            IMovieAppService movieAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _movieAppService = movieAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("movie-management/pagination_test")]
        public IActionResult PaginationTest(int skip, int take)
        {
            return Response(_movieAppService.GetAll(skip, take));
        }

        [HttpGet]
        [Route("movie-management/pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(_movieAppService.GetAll(skip, take));
        }
    }
}
