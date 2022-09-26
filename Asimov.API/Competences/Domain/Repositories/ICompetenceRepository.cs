using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Competences.Domain.Models;

namespace Asimov.API.Competences.Domain.Repositories
{
    public interface ICompetenceRepository
    {
        Task<IEnumerable<Competence>> ListAsync();
        Task AddAsync(Competence competence);
        Task<Competence> FindByIdAsync(int id);
        Task<IEnumerable<Competence>> FindByCourseId(int courseId);
        Task<Competence> FindByTitleAsync(string title);
        public bool ExistByTitle(string title);
        void Update(Competence competence);
        void Remove(Competence competence);
    }
}