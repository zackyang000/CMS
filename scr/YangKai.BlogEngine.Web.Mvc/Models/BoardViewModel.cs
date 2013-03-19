using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YangKai.BlogEngine.Modules.BoardModule.Objects;

namespace YangKai.BlogEngine.Web.Mvc.Models
{
    public class BoardViewModel
    {
        public Guid BoardId { get; set; }
        public int? Index { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public static class BoardViewModelExtension
    {
        public static Board ToBoardEntity(this BoardViewModel viewModel)
        {
            return new Board()
                {
                    Author = viewModel.Author,
                    Content = viewModel.Content,
                };
        }

        public static BoardViewModel ToBoardViewModel(this Board entity)
        {
           return new BoardViewModel()
            {
                BoardId = entity.BoardId,
                Author = entity.Author,
                Content = entity.Content,
                CreateDate = entity.CreateDate,
            };
        }

        public static IList<BoardViewModel> ToBoardViewModels(this IList<Board> entities)
        {
            var list = new List<BoardViewModel>();
            var index = entities.Count;
            entities.ToList().ForEach(p =>
                {
                    var viewModel = p.ToBoardViewModel();
                    viewModel.Index = index--;
                    list.Add(viewModel);
                });
            return list;
        }
    }
}