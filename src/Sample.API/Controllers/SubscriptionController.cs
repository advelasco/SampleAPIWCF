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
    [Route("api/v{version:apiVersion}/Subscriptions")]
    public class SubscriptionController : Controller
    {
        public readonly ISubscriptionMediator _subscriptionMediator;

        public SubscriptionController(ISubscriptionMediator subscriptionMediator)
        {
            _subscriptionMediator = subscriptionMediator;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of available users.</returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subscription>))]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var subscription = _subscriptionMediator.GetAll();

                if (subscription != null)
                {
                    var subscriptionModel = subscription.Select(subs => new Subscription
                    {
                        Id = subs.Id,
                        CallMinutes = subs.CallMinutes,
                        Name = subs.Name,
                        Price = subs.Price,
                        PriceIncVatAmount = subs.PriceIncVatAmount
                    });

                    return Ok(subscriptionModel);
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
        [ProducesResponseType(200, Type = typeof(Subscription))]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var subscription = _subscriptionMediator.GetById(id);

                if (subscription != null)
                {
                    return Ok(new Subscription()
                    {
                        Id = subscription.Id,
                        CallMinutes = subscription.CallMinutes,
                        Name = subscription.Name,
                        Price = subscription.Price,
                        PriceIncVatAmount = subscription.PriceIncVatAmount
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
        [ProducesResponseType(201, Type = typeof(IEnumerable<Subscription>))]
        [HttpPost]
        public IActionResult Post([FromBody]SubscriptionPost value)
        {
            if (value != null)
            {
                try
                {
                    _subscriptionMediator.Insert(new SubscriptionDTO
                    {
                        Name = value.Name,
                        Price = value.Price,
                        PriceIncVatAmount = value.PriceIncVatAmount,
                        CallMinutes = value.CallMinutes
                    });
                }
                catch (System.Exception)
                {
                    return BadRequest(value);
                }
            }

            return Created("/subscriptions", value);
        }

        /// <summary>
        /// Add subscription to user
        /// </summary>
        /// <returns>Add subscription to user.</returns>
        [ProducesResponseType(204)]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]SubscriptionPost value)
        {
            try
            {
                var subscription = _subscriptionMediator.GetById(id);

                if(subscription != null)
                {
                    subscription.CallMinutes = value.CallMinutes;
                    subscription.Name = value.Name;
                    subscription.Price = value.Price;
                    subscription.PriceIncVatAmount = value.PriceIncVatAmount;

                    _subscriptionMediator.Update(subscription);
                }

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
        public IActionResult Delete(Guid id)
        {
            try
            {
                _subscriptionMediator.Delete(id);
                return Accepted();
            }
            catch (System.Exception)
            {
                return BadRequest(id);
            }
        }
    }
}