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
using TouristAgency.Model;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for TourGuideReviewCreation.xaml
    /// </summary>
    public partial class TourGuideReviewCreation : Window
    {
        public TourGuideReviewCreation(Tourist tourist)
        {
            InitializeComponent();
        }
    }
}