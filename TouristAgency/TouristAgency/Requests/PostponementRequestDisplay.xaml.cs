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
using TouristAgency.Users;

namespace TouristAgency.Requests
{
    /// <summary>
    /// Interaction logic for PostponementRequestDisplay.xaml
    /// </summary>
    public partial class PostponementRequestDisplay : UserControl
    {
        public PostponementRequestDisplay()
        {
            InitializeComponent();
            //DataContext = new PostponementRequestDisplayViewModel(loggedInGuest, this);
        }
    }
}
