﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public string Id { get; }
        public string Title { get; }
        public string Author { get; }
        public bool IsCheckedOut { get; private set; }
        public DateTime? CheckedOutDate { get; set; }
        public Book(string id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
            IsCheckedOut = false;
        }

        public void CheckOut()
        {
            IsCheckedOut = true;
            CheckedOutDate = DateTime.Now;
        }

        public void Return()
        {
            IsCheckedOut = false;
            CheckedOutDate = null;
        }

        
    }
}
