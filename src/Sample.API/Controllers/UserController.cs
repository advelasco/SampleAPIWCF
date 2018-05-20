namespace Sample.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sample.API.Models;
    using Sample.DTO;
    using Sample.Mediator.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Users")]
    public class UserController : Controller
    {
        public readonly IUserMediator _userMediator;

        public UserController(IUserMediator userMediator)
        {
            _userMediator = userMediator;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of available users.</returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _userMediator.GetAll();

                if (users != null)
                {
                    return Ok(users.Select(user => new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Subscription = user.Subscriptions.Select(subs => new Subscription
                        {
                            Id = subs.Id,
                            CallMinutes = subs.CallMinutes,
                            Name = subs.Name,
                            Price = subs.Price,
                            PriceIncVatAmount = subs.PriceIncVatAmount
                        }),
                        TotalCallMinutes = user.TotalCallMinutes,
                        TotalPriceIncVatAmount = user.TotalPriceIncVatAmount
                    }));
                }
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return NotFound();
        }

        /// <summary>
        /// Get current user by id
        /// </summary>
        /// <returns>Get user by id.</returns>
        [ProducesResponseType(200, Type = typeof(User))]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var user = _userMediator.GetById(id);

                if(user != null)
                {
                    return Ok(new User()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Subscription = user.Subscriptions.Select(subs => new Subscription
                        {
                            Id = subs.Id,
                            CallMinutes = subs.CallMinutes,
                            Name = subs.Name,
                            Price = subs.Price,
                            PriceIncVatAmount = subs.PriceIncVatAmount
                        }),
                        TotalCallMinutes = user.TotalCallMinutes,
                        TotalPriceIncVatAmount = user.TotalPriceIncVatAmount
                    });
                }
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return NotFound();
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <returns>Create a new user.</returns>
        [ProducesResponseType(201, Type = typeof(IEnumerable<User>))]
        [HttpPost]
        public IActionResult Post([FromBody]UserPost value)
        {
            if (value != null)
            {
                try
                {
                    _userMediator.Insert(new UserDTO
                    {
                        Email = value.Email,
                        Firstname = value.Firstname,
                        Lastname = value.Lastname
                    });
                }
                catch (System.Exception)
                {
                    return BadRequest(value);
                }
            }

            return Created("/users", value);
        }

        /// <summary>
        /// Add subscription to user
        /// </summary>
        /// <returns>Add subscription to user.</returns>
        [ProducesResponseType(204)]
        [HttpPut("{id}/{subscriptionId}")]
        public IActionResult Put(long id, Guid subscriptionId)
        {
            try
            {
                _userMediator.UpdateSubscription(id, subscriptionId);
                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest(id);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <returns>Delete user.</returns>
        [ProducesResponseType(202)]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _userMediator.Delete(id);
                return Accepted();
            }
            catch (System.Exception)
            {
                return BadRequest(id);
            }
        }
    }
}