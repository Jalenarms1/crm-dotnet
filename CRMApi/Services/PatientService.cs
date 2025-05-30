using System;
using CarelinkModels;
using CRMApi.Data;

namespace CRMApi.Services;

public class PatientService
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetPatientsAsync()
    {
        var patients = _context.Patients.ToList();

        return patients;
    }
}
