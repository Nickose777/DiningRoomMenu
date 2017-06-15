using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.CategoryControls.ViewModels
{
    public class CategoryListViewModel : ObservableObject, IObserver
    {
        public event GenericEventHandler<CategoryDisplayDTO> CategorySelected;

        private readonly IControllerFactory factory;
        private CategoryDisplayDTO category;

        public CategoryListViewModel(IControllerFactory factory, ICategorySubject subject)
        {
            this.factory = factory;
            subject.Subscribe(this);

            this.SelectCommand = new DelegateCommand(
                () => RaiseCategorySelectedEvent(Category),
                obj => Category != null);

            this.Categories = new ObservableCollection<CategoryDisplayDTO>();

            Update();
        }

        public void Update()
        {
            Categories.Clear();

            using (ICategoryController controller = factory.CreateCategoryController())
            {
                DataControllerMessage<IEnumerable<CategoryDisplayDTO>> controllerMessage = controller.GetAll();

                if (controllerMessage.IsSuccess)
                {
                    foreach (CategoryDisplayDTO category in controllerMessage.Data)
                    {
                        Categories.Add(category);
                    }
                }
            }
        }

        public ICommand SelectCommand { get; private set; }

        public CategoryDisplayDTO Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisePropertyChangedEvent("Category");
            }
        }

        public ObservableCollection<CategoryDisplayDTO> Categories { get; set; }

        private void RaiseCategorySelectedEvent(CategoryDisplayDTO category)
        {
            var handler = CategorySelected;
            if (handler != null)
            {
                GenericEventArgs<CategoryDisplayDTO> e = new GenericEventArgs<CategoryDisplayDTO>(category);
                handler(this, e);
            }
        }
    }
}
