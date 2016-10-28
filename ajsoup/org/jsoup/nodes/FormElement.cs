using System;
using System.Collections.Generic;
using Org.Jsoup.Helper;

namespace Org.Jsoup.Nodes {
    /// <summary>A HTML Form Element provides ready access to the form fields/controls that are associated with it.
    ///     </summary>
    public class FormElement : Element {
        private readonly Org.Jsoup.Select.Elements elements = new Org.Jsoup.Select.Elements();

        /// <summary>Create a new, standalone form element.</summary>
        /// <param name="tag">tag of this element</param>
        /// <param name="baseUri">the base URI</param>
        /// <param name="attributes">initial attributes</param>
        public FormElement(Org.Jsoup.Parser.Tag tag, String baseUri, Attributes attributes)
            : base(tag, baseUri, attributes) {
        }

        /// <summary>Get the list of form control elements associated with this form.</summary>
        /// <returns>form controls associated with this element.</returns>
        public virtual Org.Jsoup.Select.Elements Elements() {
            return elements;
        }

        /// <summary>Add a form control element to this form.</summary>
        /// <param name="element">form control to add</param>
        /// <returns>this form element, for chaining</returns>
        public virtual Org.Jsoup.Nodes.FormElement AddElement(Element element) {
            elements.Add(element);
            return this;
        }

        //    /**
        //     * Prepare to submit this form. A Connection object is created with the request set up from the form values. You
        //     * can then set up other options (like user-agent, timeout, cookies), then execute it.
        //     * @return a connection prepared from the values of this form.
        //     * @throws IllegalArgumentException if the form's absolute action URL cannot be determined. Make sure you pass the
        //     * document's base URI when parsing.
        //     */
        //    public Connection submit() {
        //        String action = hasAttr("action") ? absUrl("action") : baseUri();
        //        Validate.notEmpty(action, "Could not determine a form action URL for submit. Ensure you set a base URI when parsing.");
        //        Connection.Method method = attr("method").toUpperCase().equals("POST") ?
        //                Connection.Method.POST : Connection.Method.GET;
        //
        //        return Jsoup.connect(action)
        //                .data(formData())
        //                .method(method);
        //    }
        /// <summary>Get the data that this form submits.</summary>
        /// <remarks>
        /// Get the data that this form submits. The returned list is a copy of the data, and changes to the contents of the
        /// list will not be reflected in the DOM.
        /// </remarks>
        /// <returns>a list of key vals</returns>
        public virtual IList<KeyVal> FormData() {
            List<KeyVal> data = new List<KeyVal>();
            // iterate the form control elements and accumulate their values
            foreach (Element el in elements) {
                if (!el.Tag().IsFormSubmittable()) {
                    continue;
                }
                // contents are form listable, superset of submitable
                if (el.HasAttr("disabled")) {
                    continue;
                }
                // skip disabled form inputs
                String name = el.Attr("name");
                if (name.Length == 0) {
                    continue;
                }
                String type = el.Attr("type");
                if ("select".Equals(el.TagName())) {
                    Org.Jsoup.Select.Elements options = el.Select("option[selected]");
                    bool set = false;
                    foreach (Element option in options) {
                        data.Add(KeyVal.Create(name, option.Val()));
                        set = true;
                    }
                    if (!set) {
                        Element option_1 = el.Select("option").First();
                        if (option_1 != null) {
                            data.Add(KeyVal.Create(name, option_1.Val()));
                        }
                    }
                }
                else {
                    if ("checkbox".EqualsIgnoreCase(type) || "radio".EqualsIgnoreCase(type)) {
                        // only add checkbox or radio if they have the checked attribute
                        if (el.HasAttr("checked")) {
                            String val = el.Val().Length > 0 ? el.Val() : "on";
                            data.Add(KeyVal.Create(name, val));
                        }
                    }
                    else {
                        data.Add(KeyVal.Create(name, el.Val()));
                    }
                }
            }
            return data;
        }
    }
}
