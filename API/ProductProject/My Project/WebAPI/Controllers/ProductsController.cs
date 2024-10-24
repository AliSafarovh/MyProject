﻿using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _productService.GetAll();
            {
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
        }
        [HttpPost("add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id) 
        {
            var result = _productService.GetById(id);
            if (result.Success) 
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromForm]Product product)
        {
            var result =_productService.Delete(product);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
