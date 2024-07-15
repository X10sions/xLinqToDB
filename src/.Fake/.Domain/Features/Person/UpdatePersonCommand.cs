﻿using System.ComponentModel.DataAnnotations;

namespace X10sions.Fake.Features.Person;

public class UpdatePersonCommand {
    public int Id { get; set; }
    [Required][Display(Name = "First Name")] public string FirstName { get; set; }
    [Required][Display(Name = "Last Name")] public string LastName { get; set; }
    [Required][Display(Name = "Email")] public string Email { get; set; }
    [Required][Display(Name = "Mobile Number")] public string MobileNo { get; set; }
  }

