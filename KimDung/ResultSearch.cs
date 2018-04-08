﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controller
{
    class ResultSearch
    {
        private string Content;
        private string Correct;

        public ResultSearch(string content, string correct)
        {
            this.Content = content;
            this.Correct = correct;
        }

        public string CONTENT
        {
            get { return Content; }
            set { Content = value; }
        }

        public string CORRECT
        {
            get { return Correct; }
            set { Correct = value; }
        }

        public static ResultSearch Search(string input, string chapter, int numberChapter)
        {
            ResultSearch resultSearch = new ResultSearch("" ,"");
            Regex splitChapter = new Regex("(\\w|\\040|\\t|\\f|\\v|" + "\\" + "\"" + "|\\“|\\”|\\„|\\,|\\-){1,}(\\W|\\z)");
            Regex splitInput = new Regex("\\w{1,}(\\s|\\W|\\z)");
            Regex formatInput = new Regex("\\w{1,}");

            int count = splitInput.Matches(input).Count;
            string[] str = new string[count];

            int index = 0;

            foreach (Match item in splitInput.Matches(input))
            {
                foreach (Match item2 in formatInput.Matches(item.ToString()))
                {
                    str[index] = item2.ToString().ToUpper();
                }
                index++;
            }

            String tmp = "(";
            for (int i = 0; i < str.Length; i++)
            {
                if (i == str.Length - 1)
                {
                    tmp += str[i].ToUpper();
                }
                else
                {
                    tmp += str[i].ToUpper() + "|";
                }
            }
            tmp += ")";

            string title = "";
            foreach (Match item in splitChapter.Matches(chapter))
            {
                bool check = true;
                for (int i = 0; i < str.Length; i++)
                {
                        if (!item.ToString().ToUpper().Contains("" + str[i].ToUpper()))
                    {
                        check = false;
                        break;
                    }
                }

                if (check == true)
                {
                    //MessageBox.Show("Chapter " + (numberChapter + 1) + ": " + title + "\n" + item.ToString());
                    resultSearch.Content = "Chapter " + (numberChapter + 1) + ": " + title + "\n" + item.ToString();
                    resultSearch.Correct = tmp;

                    break;
                }
            }

                return resultSearch;
        }
    }
}
