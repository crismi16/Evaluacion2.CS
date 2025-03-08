using System.Collections.Generic;

namespace Evaluacion2.Models
{
    public class Breed
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public BreedAttributes Attributes { get; set; }
        public BreedRelationships Relationships { get; set; }
        public string ImageUrl { get; set; }
    }

    public class BreedAttributes
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Life Life { get; set; }
        public Weight Male_weight { get; set; }
        public Weight Female_weight { get; set; }
        public bool Hypoallergenic { get; set; }
    }

    public class Life
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }

    public class Weight
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }

    public class BreedRelationships
    {
        public Group Group { get; set; }
    }

    public class Group
    {
        public GroupData Data { get; set; }
    }

    public class GroupData
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }

    public class BreedResponse
    {
        public List<Breed> Data { get; set; }
        public Meta Meta { get; set; }
        public Links Links { get; set; }
    }

    public class Meta
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int Current { get; set; }
        public int Next { get; set; }
        public int Last { get; set; }
        public int Records { get; set; }
    }

    public class Links
    {
        public string Self { get; set; }
        public string Current { get; set; }
        public string Next { get; set; }
        public string Last { get; set; }
    }
}