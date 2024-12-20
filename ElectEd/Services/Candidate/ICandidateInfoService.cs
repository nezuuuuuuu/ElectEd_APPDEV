﻿using ElectEd.DTO;

namespace ElectEd.Services.Candidate
{
    public interface ICandidateInfoService
    {
        Task<List<CandidateDtoWithId>> GetCandidates();

        CandidateDtoWithId? GetCandidateById(int id);
        Task<List<CandidateDtoWithId>> GetCandidatesByElectionId(int electionId);

    }
}
