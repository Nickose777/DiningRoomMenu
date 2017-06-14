using DiningRoomMenu.Logic.DTO.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.CategoryControls.ViewModels
{
    public class CategoryListViewModel : ObservableObject
    {
        public CategoryListViewModel(IEnumerable<CategoryDisplayDTO> categories)
        {
            this.Categories = new ObservableCollection<CategoryDisplayDTO>(categories);
        }

        public ObservableCollection<CategoryDisplayDTO> Categories { get; set; }
    }
}
