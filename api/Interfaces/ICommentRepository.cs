using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comments;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(int stockId, CreateCommentDto createCommentDto);
        Task<Comment?> UpdateAsync(int id, Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}