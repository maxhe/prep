using System;
using System.Collections.Generic;
using prep.utility;
using prep.utility.filtering;

namespace prep.collections
{
  public class MovieLibrary
  {
    IList<Movie> movies;

    public MovieLibrary(IList<Movie> list_of_movies)
    {
      this.movies = list_of_movies;
    }

    public IEnumerable<Movie> all_movies()
    {
      return movies.one_at_a_time();
    }

    public void add(Movie movie)
    {
      if (already_contains(movie)) return;
      movies.Add(movie);
    }

    bool already_contains(Movie movie)
    {
      return movies.Contains(movie);
    }

    public IEnumerable<Movie> sort_all_movies_by_title_descending()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Movie> search_movies_by_criteria(Condition<Movie> condition)
    {
      return movies.all_items_matching(new Match<Movie>(condition));
    }

    public class Match<Item> : IMatchAn<Item>
    {
      private Condition<Item> _condition;
 
      public Match(Condition<Item> condition)
      {
        _condition = condition;
      }
      public bool matches(Item item)
      {
        return _condition(item);
      }
    }

    public IEnumerable<Movie> all_movies_published_by_pixar()
    {
      return search_movies_by_criteria(x => x.production_studio == ProductionStudio.Pixar);
    }


    public IEnumerable<Movie> all_movies_published_by_pixar_or_disney()
    {
      return search_movies_by_criteria(x => x.production_studio == ProductionStudio.Pixar ||
        x.production_studio == ProductionStudio.Disney);
    }

    public IEnumerable<Movie> all_movies_published_after(int year)
    {
      return search_movies_by_criteria(m => m.date_published.Year > year);
    }

    public IEnumerable<Movie> all_movies_not_published_by_pixar()
    {
      return search_movies_by_criteria(m => m.production_studio != ProductionStudio.Pixar);
    }

    public IEnumerable<Movie> all_movies_published_between_years(int startingYear, int endingYear)
    {
      return search_movies_by_criteria(m => m.date_published.Year >= startingYear && m.date_published.Year <= endingYear);
    }

    public IEnumerable<Movie> all_kid_movies()
    {
      return search_movies_by_criteria(m => m.genre == Genre.kids);
    }

    public IEnumerable<Movie> all_action_movies()
    {
      return search_movies_by_criteria(m => m.genre == Genre.action);
    }

    public IEnumerable<Movie> sort_all_movies_by_date_published_descending()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Movie> sort_all_movies_by_date_published_ascending()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Movie> sort_all_movies_by_title_ascending()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Movie> sort_all_movies_by_movie_studio_and_year_published()
    {
      throw new NotImplementedException();
    }
  }
}