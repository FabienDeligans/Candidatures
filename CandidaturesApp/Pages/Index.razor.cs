using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using CandidaturesApp.Pages.CandidaturePage;
using CandidaturesCore.Controller;
using CandidaturesCore.Model;
using Microsoft.AspNetCore.Components;

namespace CandidaturesApp.Pages
{
    public partial class Index
    {
        private List<Candidature> Candidatures { get; set; } = new List<Candidature>();
        private List<Candidature> CandidaturesFiltre { get; set; }

        public string CandidatureId { get; set; }

        protected override void OnInitialized()
        {
            var controller = new BaseController<Candidature>();
            Candidatures = controller.QueryCollection().OrderBy(v => v.Date).ToList();
            CandidaturesFiltre = Candidatures;
        }

        private async Task Create()
        {
            var modalForm = Modal.Show<Edit>("Nouvelle candidature");
            var result = await modalForm.Result;

            if (!result.Cancelled)
            {
                OnInitialized();
            }
        }

        private async Task Edit(string id)
        {
            var parameter = new ModalParameters();
            parameter.Add(nameof(Pages.CandidaturePage.Edit.CandidatureId), id);
            var modalForm = Modal.Show<Edit>("Update Candidature", parameter);
            var result = await modalForm.Result;

            if (!result.Cancelled)
            {
                OnInitialized();
            }
        }

        private async Task Search(ChangeEventArgs e)
        {
            var parameter = new ModalParameters();
            parameter.Add(nameof(Pages.CandidaturePage.Edit.CandidatureId), e.Value.ToString());
            var modalForm = Modal.Show<Edit>("Update Candidature", parameter);
            var result = await modalForm.Result;

            if (!result.Cancelled)
            {
                OnInitialized();
            }
        }

        private void Filtre(ChangeEventArgs e)
        {
            CandidaturesFiltre = Candidatures
                .Where(v => v.DateEtat >= DateTime.Parse(e.Value.ToString() ?? string.Empty))
                .ToList(); 
            StateHasChanged();
        }

    }
}
