using System.Collections.Generic;

namespace DocOffice.Models
{
  public class Specialty
  {
    public Specialty()
    {
      this.JoinEntSpec = new HashSet<DoctorSpecialty>();
    }

    public int SpecialtyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<DoctorSpecialty> JoinEntSpec { get; set; }
  }
}