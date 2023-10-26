using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrueba.Interfaces;
using ProyectoPrueba.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace ProyectoPrueba.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRespository;
        public ProductController( IProductRepository productRespository)
        {
            this._productRespository = productRespository;
        }
        private bool ValidarSlug(string slug)
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

                if (claimsIdentity == null || claimsIdentity.Claims.Count() == 0) return false;

                var slugIdentity = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "slug").Value;

                if (slugIdentity.Equals(slug)) return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        [Route("{slug}/products")]
        public IActionResult index(string slug)
        {
            if( ! ValidarSlug(slug))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            IList<Product> products = this._productRespository.getAll(slug);

            return StatusCode(StatusCodes.Status200OK, new { result = "OK", type = slug, products = products });
        }
        [HttpPost]
        [Route("{slug}/products")]
        public IActionResult insert(string slug, Product product)
        {
            try
            {
                if (!ValidarSlug(slug))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                int productId = this._productRespository.insert(slug, product);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", type = slug, id = productId});
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }

        [HttpPut]
        [Route("{slug}/products")]
        public IActionResult update(string slug, Product product)
        {
            try
            {

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }

            if (!ValidarSlug(slug))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            int productId = this._productRespository.update(slug, product);

            return StatusCode(StatusCodes.Status200OK, new { result = "OK", type = slug, id = productId });
        }

        [HttpDelete]
        [Route("{slug}/products/{productId}")]
        public IActionResult delete(string slug, int productId)
        {
            if (!ValidarSlug(slug))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            this._productRespository.delete(slug, productId);

            return StatusCode(StatusCodes.Status200OK, new { result = "OK", type = slug});
        }

    }
}
