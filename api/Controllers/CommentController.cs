using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost ("{stockId}")]
        public async Task<ActionResult> CreateAsync([FromRoute] int stockId, CreateCommentDto createDto)
        {
            var stock = await _stockRepo.GetByIdAsync(stockId);
            if (stock == null)
            {
                return BadRequest("Stock Id does not exist");
            }
            var comment = await _commentRepo.CreateAsync(stockId, createDto);
            return CreatedAtAction(nameof(GetByIdAsync), new {id = comment.Id}, comment.ToCommentDto());
        }
    }
}