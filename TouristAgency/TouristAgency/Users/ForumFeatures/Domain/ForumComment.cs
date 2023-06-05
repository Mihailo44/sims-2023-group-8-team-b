using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Users.Domain;
using TouristAgency.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TouristAgency.Users.ForumFeatures.Domain
{
    public class ForumComment : ISerializable,INotifyPropertyChanged,IValidate
    {
        private int _id;
        private Forum _forum;
        private User _user;
        private string _comment;
        private DateTime _created;
        private int _reportNum;
        private bool _superComment;
        
        public ForumComment()
        {
            _id = -1;
            _created = DateTime.Now;
            Forum = new();
            User = new();
            SuperComment = false;
        }

        public ForumComment(User user,Forum forum,string comment)
        {
            _id = -1;
            _created = DateTime.Now;
            _user = user;
            _forum = forum;
            _comment = comment;
            SuperComment = false;
        }

        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                }
            }
        }

        public Forum Forum
        {
            get => _forum;
            set
            {
                if(_forum != value)
                {
                    _forum = value;
                }
            }
        }

        public User User
        {
            get => _user;
            set
            {
                if(_user != value)
                {
                    _user = value;
                }
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if(_comment != value)
                {
                    _comment = value;
                }
            }
        }

        public bool SuperComment
        {
            get => _superComment;
            set
            {
                if (_superComment != value)
                {
                    _superComment = value;
                }
            }
        }

        public DateTime Created
        {
            get => _created;
            set
            {
                if(_created != value)
                {
                    _created = value;
                }
            }
        }

        public int ReportNum
        {
            get => _reportNum;
            set
            {
                if (_reportNum != value)
                {
                    _reportNum = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<string, string> _validationErrors = new()
        {
            {"Comment",string.Empty},
        };

        public Dictionary<string, string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
            set
            {
                _validationErrors = value;
                OnPropertyChanged();
            }
        }

        public void ValidateSelf(string comment)
        {
            ValidationClear();

            if (string.IsNullOrEmpty(comment))
            {
                ValidationErrors["Comment"] = "Please enter a comment";
            }

            OnPropertyChanged(nameof(ValidationErrors));
        }

        public void ValidationClear()
        {
            ValidationErrors["Comment"] = string.Empty;
            OnPropertyChanged(nameof(ValidationErrors));
        }

        public bool IsValid
        {
            get
            {
                foreach (var key in ValidationErrors.Keys)
                {
                    if (!string.IsNullOrEmpty(ValidationErrors[key]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Forum.Id = int.Parse(values[1]);
            User.ID = int.Parse(values[2]);
            Comment = values[3];
            Created = DateTime.Parse(values[4]);
            ReportNum = int.Parse(values[5]);
            SuperComment = bool.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Forum.Id.ToString(),
                User.ID.ToString(),
                Comment,
                Created.ToString(),
                ReportNum.ToString(),
                SuperComment.ToString()
            };

            return csvValues;
        }
    }
}
