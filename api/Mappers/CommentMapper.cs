using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comments;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            var commentDto = new CommentDto {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };

            return commentDto;
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentDto dto)
        {
            var comment = new Comment {
                Title = dto.Title,
                Content = dto.Content
            };

            return comment;
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentDto dto)
        {
            return new Comment {
                Title = dto.Title,
                Content = dto.Content
            };

        }
    }
}