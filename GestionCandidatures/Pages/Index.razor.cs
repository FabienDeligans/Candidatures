using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using CandidaturesCore.Controller;
using CandidaturesCore.Model;
using GestionCandidatures.Pages.CandidaturePage;
using Microsoft.AspNetCore.Components;

namespace GestionCandidatures.Pages
{
    public partial class Index
    {
        private List<Candidature> Candidatures { get; set; } = new List<Candidature>();

        protected override void OnInitialized()
        {
            var controller = new BaseController<Candidature>();
            Candidatures = controller.QueryCollection().OrderBy(v => v.Date).ToList();
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
    }
}
