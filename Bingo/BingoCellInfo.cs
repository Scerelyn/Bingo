﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    /// <summary>
    /// Acts as the backing data context for bingo sheet cells
    /// </summary>
    public class BingoCellInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// The text to display on the cell
        /// </summary>
        public string CellText
        {
            get { return cellText; }
            set
            {
                cellText = value;
                FieldChanged();
            }
        }
        private string cellText = "";

        /// <summary>
        /// Determines whether or not the cell is checked
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                FieldChanged();
            }
        }
        private bool isChecked = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void FieldChanged(string field = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(field));
            }
        }
    }
}
