﻿using Core.CommunityClasses;
using Core.NewsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class NewsDTO : PostDTO
    {
        public int NewsId { get; set; } //News
        public NewsCategory NewsCategory { get; set; } //News
        public List<NewsCommentDTO> NewsComments { get; set; } //News

    }
}
