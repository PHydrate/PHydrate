<<<<<<< HEAD
﻿using System.Dynamic;

namespace Machine.Specifications.Example.Clr4
{
	public class When_specs_target_the_common_language_runtime_in_version_4
	{
		static ExpandoObject ExpandoObject;

		Because of = () => { ExpandoObject = new ExpandoObject(); };

		It should_be_able_to_use_components_that_are_available_in_the_target_framework =
			() => ExpandoObject.ShouldNotBeNull();
	}
=======
﻿using System.Dynamic;

namespace Machine.Specifications.Example.Clr4
{
	public class When_specs_target_the_common_language_runtime_in_version_4
	{
		static ExpandoObject ExpandoObject;

		Because of = () => { ExpandoObject = new ExpandoObject(); };

		It should_be_able_to_use_components_that_are_available_in_the_target_framework =
			() => ExpandoObject.ShouldNotBeNull();
	}
>>>>>>> feature/externs-subtree
}