using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.CategoryControls.ViewModels
{
    public class CategoryEditViewModel : ObservableObject
    {
        public event GenericEventHandler<CategoryEditDTO> CategorySaveRequest;
        public event GenericEventHandler<CategoryEditDTO> CategoryDeleteRequest;

        private readonly CategoryEditDTO category;

        public CategoryEditViewModel(CategoryEditDTO category)
        {
            this.category = category;

            this.SaveCommand = new DelegateCommand(
                () => RaiseCategorySaveRequestEvent(category),
                obj => CanSave());
            this.DeleteCommand = new DelegateCommand(() => RaiseCategoryDeleteRequestEvent(category));

            this.Dishes = new ObservableCollection<string>(category.Dishes);
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public string Name
        {
            get { return category.NewName; }
            set
            {
                category.NewName = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public ObservableCollection<string> Dishes { get; private set; }

        private bool CanSave()
        {
            return
                !String.IsNullOrEmpty(Name) &&
                category.NewName != category.OldName;
        }

        private void RaiseCategorySaveRequestEvent(CategoryEditDTO category)
        {
            var handler = CategorySaveRequest;
            if (handler != null)
            {
                GenericEventArgs<CategoryEditDTO> e = new GenericEventArgs<CategoryEditDTO>(category);
                handler(this, e);
            }
        }

        private void RaiseCategoryDeleteRequestEvent(CategoryEditDTO category)
        {
            var handler = CategoryDeleteRequest;
            if (handler != null)
            {
                GenericEventArgs<CategoryEditDTO> e = new GenericEventArgs<CategoryEditDTO>(category);
                handler(this, e);
            }
        }
    }
}
