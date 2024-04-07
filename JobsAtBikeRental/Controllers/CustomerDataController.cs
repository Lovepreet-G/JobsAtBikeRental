using JobsAtBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JobsAtBikeRental.Controllers
{
    public class CustomerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all customers in the system.
        /// </summary>
        /// <returns>
        /// A list of customerDto objects representing the customers.
        /// </returns>
        /// <example>
        /// Get: api/CustomerData/ListCustomers
        /// </example>
        [HttpGet]
        [Route("api/CustomerData/ListCustomers")]
        public List<customerDto> ListCustomers()
        {
            List<Customer> customers = db.Customers.ToList();
            List<customerDto> customerDtos = new List<customerDto>();

            customers.ForEach(c => customerDtos.Add(new customerDto()
            {
                customerId = c.customerId,
                customerName = c.customerName,
                customerAddress = c.customerAddress,
                customerPhone = c.customerPhone
            }));

            return customerDtos;
        }

        /// <summary>
        /// Retrieves information about a specific customer.
        /// </summary>
        /// <param name="id">The ID of the customer to find.</param>
        /// <returns>
        /// The customerDto object representing the customer with the specified ID.
        /// </returns>
        /// <example>
        /// get : api/CustomerData/FindCustomer/1
        /// </example>
        [ResponseType(typeof(Customer))]
        [HttpGet]
        [Route("api/CustomerData/FindCustomer/{id}")]
        public IHttpActionResult FindCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customerDto customerDto = new customerDto()
            {
                customerId = customer.customerId,
                customerName = customer.customerName,
                customerAddress = customer.customerAddress,
                customerPhone = customer.customerPhone
            };


            return Ok(customerDto);
        }

        /// <summary>
        /// Updates information for a specific customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customer">The updated customer object.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// Post : api/CustomerData/UpdateCustomer/1
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/CustomerData/UpdateCustomer/{id}")]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            Debug.WriteLine("I have reached the update customer method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != customer.customerId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + customer.customerId);
                Debug.WriteLine("POST parameter" + customer.customerName);
                Debug.WriteLine("POST parameter " + customer.customerAddress);
                Debug.WriteLine("POST parameter " + customer.customerPhone);
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
                {
                    Debug.WriteLine("customer not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a new customer to the system.
        /// </summary>
        /// <param name="customer">The customer object to add.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/CustomerData/AddCustomer 
        /// </example>
        [ResponseType(typeof(bike))]
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.customerId }, customer);
        }

        /// <summary>
        /// Deletes a customer from the system.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>

        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Checks if a customer exists in the system based on its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to check.</param>
        /// <returns>True if the customer exists, otherwise false.</returns>
        private bool customerExists(int id)
        {
            return db.Customers.Count(c => c.customerId == id) > 0;
        }
    }
}
