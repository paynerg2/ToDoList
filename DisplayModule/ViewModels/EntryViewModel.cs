using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using ToDoList.Data.Models;
using ToDoList.Data.Services;

namespace DisplayModule.ViewModels
{
    public class EntryViewModel : BindableBase
    {
        private IEntryRepository _repository;
        private Entry _selectedEntry;
        private List<Entry> _entries;
        private bool _editSectionIsVisible = false;

        public DelegateCommand CompleteCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        public Entry SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }

        public List<Entry> Entries
        {
            get { return _entries; }
            set { SetProperty(ref _entries, value); }
        }
        
        public bool EditSectionIsVisible
        {
            get { return _editSectionIsVisible; }
            set { SetProperty(ref _editSectionIsVisible, value); }
        }


        public EntryViewModel(IEntryRepository repository)
        {
            _repository = repository;

            CompleteCommand = new DelegateCommand(Complete);
            DeleteCommand = new DelegateCommand(Delete);
            EditCommand = new DelegateCommand(Edit);

            Initialize();
        }

        private async void Edit()
        {
            await _repository.UpdateEntryAsync(SelectedEntry);
            Initialize();
        }

        private async void Delete()
        {
            await _repository.DeleteEntryAsync(SelectedEntry.Id);
            Initialize();
        }

        private void Complete()
         {
            SelectedEntry.IsCompleted = !SelectedEntry.IsCompleted;
        }

        private async void Initialize()
        {
            Entries = await _repository.GetEntriesAsync();
        }
    }
}
