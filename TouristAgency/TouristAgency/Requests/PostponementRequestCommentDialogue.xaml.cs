﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TouristAgency.Requests;
using TouristAgency.Requests.Domain;

namespace TouristAgency.View.Dialogue
{
    /// <summary>
    /// Interaction logic for PostponementRequestCommentDialogue.xaml
    /// </summary>
    public partial class PostponementRequestCommentDialogue : Window
    {
        public PostponementRequestCommentDialogue(PostponementRequest postponementRequest)
        {
            InitializeComponent();
            DataContext = new PostponementRequestCommentDialogueViewModel(postponementRequest,this);
        }
    }
}
