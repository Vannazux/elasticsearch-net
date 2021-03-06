﻿using System;
using System.Collections.Generic;

namespace Nest
{
	public class AnalyzeCharFilters : List<Union<string, ICharFilter>>
	{
		public AnalyzeCharFilters() { }
		public AnalyzeCharFilters(List<Union<string, ICharFilter>> tokenFilters)
		{
			if (tokenFilters == null) return;
			foreach (var v in tokenFilters) this.AddIfNotNull(v);
		}
		public AnalyzeCharFilters(string[] tokenFilters)
		{
			if (tokenFilters == null) return;
			foreach (var v in tokenFilters) this.AddIfNotNull(v);
		}

		public void Add(ICharFilter filter) => this.Add(new Union<string, ICharFilter>(filter));

		public static implicit operator AnalyzeCharFilters(CharFilterBase tokenFilter) => tokenFilter == null ? null : new AnalyzeCharFilters { tokenFilter };
		public static implicit operator AnalyzeCharFilters(string tokenFilter) => tokenFilter == null ? null : new AnalyzeCharFilters { tokenFilter };
		public static implicit operator AnalyzeCharFilters(string[] tokenFilters) => tokenFilters == null ? null : new AnalyzeCharFilters(tokenFilters);
	}

	public class AnalyzeCharFiltersDescriptor : DescriptorPromiseBase<AnalyzeCharFiltersDescriptor, AnalyzeCharFilters>
	{
		public AnalyzeCharFiltersDescriptor() : base(new AnalyzeCharFilters()) { }

		/// <summary>
		/// A reference to a token filter that is part of the mapping
		/// </summary>
		public AnalyzeCharFiltersDescriptor Name(string tokenFilter) => Assign(a => a.AddIfNotNull(tokenFilter));

		private AnalyzeCharFiltersDescriptor AssignIfNotNull(ICharFilter filter) =>
			Assign(a =>
			{
				if (filter != null) a.Add(filter);
			});

		/// <summary>
		/// The pattern_replace char filter allows the use of a regex to manipulate the characters in a string before analysis.
		/// </summary>
		public AnalyzeCharFiltersDescriptor PatternReplace(Func<PatternReplaceCharFilterDescriptor, IPatternReplaceCharFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new PatternReplaceCharFilterDescriptor()));

		/// <summary>
		/// A char filter of type html_strip stripping out HTML elements from an analyzed text.
		/// </summary>
		public AnalyzeCharFiltersDescriptor HtmlStrip(Func<HtmlStripCharFilterDescriptor, IHtmlStripCharFilter> selector = null) =>
			AssignIfNotNull(selector.InvokeOrDefault(new HtmlStripCharFilterDescriptor()));

		/// <summary>
		/// A char filter of type mapping replacing characters of an analyzed text with given mapping.
		/// </summary>
		public AnalyzeCharFiltersDescriptor Mapping(Func<MappingCharFilterDescriptor, IMappingCharFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new MappingCharFilterDescriptor()));

		/// <summary>
		/// The kuromoji_iteration_mark normalizes Japanese horizontal iteration marks (odoriji) to their expanded form.
		/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
		/// </summary>
		public AnalyzeCharFiltersDescriptor KuromojiIterationMark(Func<KuromojiIterationMarkCharFilterDescriptor, IKuromojiIterationMarkCharFilter> selector = null) =>
			AssignIfNotNull(selector?.InvokeOrDefault(new KuromojiIterationMarkCharFilterDescriptor()));

		/// <summary>
		/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
		/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
		/// </summary>
		public AnalyzeCharFiltersDescriptor IcuNormalization(Func<IcuNormalizationCharFilterDescriptor, IIcuNormalizationCharFilter> selector) =>
			AssignIfNotNull(selector?.Invoke(new IcuNormalizationCharFilterDescriptor()));
	}
}
