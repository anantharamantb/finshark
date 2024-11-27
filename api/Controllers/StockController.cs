using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _repo;
        public StockController(IStockRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var stocks = await _repo.GetAllAsync();
            var stocDtos = stocks.Select(s => s.ToStockDto());
            return Ok(stocDtos);
        }
        [HttpGet ("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var stock = await _repo.GetByIdAsync(id);
            return stock == null ? NotFound() : Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            var stockModel = createStockDto.ToStockFromCreateStockDto();
            await _repo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {Id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _repo.UpdateAsync(id, updateDto);
            if(stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete ([FromRoute] int id)
        {
            var stockModel = await _repo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}