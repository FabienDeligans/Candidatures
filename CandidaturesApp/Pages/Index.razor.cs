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
        public List<Candidature> Candidatures { get; set; } = new List<Candidature>();
        public string Search { get; set; }

        public string CandidatureId { get; set; }

        protected override void OnInitialized()
        {
            var controller = new BaseController<Candidature>();
            Candidatures = controller.QueryCollection().OrderBy(v => v.Date).ToList();
        }

        public async Task Create()
        {
            var modalForm = Modal.Show<Edit>("Nouvelle candidature");
            var result = await modalForm.Result;

            if (!result.Cancelled)
            {
                OnInitialized();
            }
        }

        public async Task Edit(string id)
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
