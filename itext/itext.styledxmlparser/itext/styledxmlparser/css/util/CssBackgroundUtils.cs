/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.StyledXmlParser.Css;

namespace iText.StyledXmlParser.Css.Util {
    public sealed class CssBackgroundUtils {
        /// <summary>
        /// Creates a new
        /// <see cref="CssBackgroundUtils"/>
        /// instance.
        /// </summary>
        private CssBackgroundUtils() {
        }

        /// <summary>Gets background property name corresponding to its type.</summary>
        /// <param name="propertyType">background property type</param>
        /// <returns>background property name</returns>
        public static String GetBackgroundPropertyNameFromType(CssBackgroundUtils.BackgroundPropertyType propertyType
            ) {
            switch (propertyType) {
                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_COLOR: {
                    return CommonCssConstants.BACKGROUND_COLOR;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_IMAGE: {
                    return CommonCssConstants.BACKGROUND_IMAGE;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_POSITION: {
                    return CommonCssConstants.BACKGROUND_POSITION;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_SIZE: {
                    return CommonCssConstants.BACKGROUND_SIZE;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_REPEAT: {
                    return CommonCssConstants.BACKGROUND_REPEAT;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_ORIGIN: {
                    return CommonCssConstants.BACKGROUND_ORIGIN;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_CLIP: {
                    return CommonCssConstants.BACKGROUND_CLIP;
                }

                case CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_ATTACHMENT: {
                    return CommonCssConstants.BACKGROUND_ATTACHMENT;
                }

                default: {
                    return CommonCssConstants.UNDEFINED_NAME;
                }
            }
        }

        /// <summary>Resolves the background property type using it's value.</summary>
        /// <param name="value">the value</param>
        /// <returns>the background property type value</returns>
        public static CssBackgroundUtils.BackgroundPropertyType ResolveBackgroundPropertyType(String value) {
            String url = "url(";
            if (value.StartsWith(url) && value.IndexOf('(', url.Length) == -1 && value.IndexOf(')') == value.Length - 
                1) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_IMAGE;
            }
            if (CssGradientUtil.IsCssLinearGradientValue(value) || CommonCssConstants.NONE.Equals(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_IMAGE;
            }
            if (CommonCssConstants.BACKGROUND_REPEAT_VALUES.Contains(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_REPEAT;
            }
            if (CommonCssConstants.BACKGROUND_ATTACHMENT_VALUES.Contains(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_ATTACHMENT;
            }
            if (CommonCssConstants.BACKGROUND_POSITION_VALUES.Contains(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_POSITION;
            }
            if ("0".Equals(value) || CssUtils.IsMetricValue(value) || CssUtils.IsRelativeValue(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_POSITION_OR_SIZE;
            }
            if (CommonCssConstants.BACKGROUND_SIZE_VALUES.Contains(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_SIZE;
            }
            if (CssUtils.IsColorProperty(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_COLOR;
            }
            if (CommonCssConstants.BACKGROUND_ORIGIN_OR_CLIP_VALUES.Contains(value)) {
                return CssBackgroundUtils.BackgroundPropertyType.BACKGROUND_ORIGIN_OR_CLIP;
            }
            return CssBackgroundUtils.BackgroundPropertyType.UNDEFINED;
        }

        public enum BackgroundPropertyType {
            BACKGROUND_COLOR,
            BACKGROUND_IMAGE,
            BACKGROUND_POSITION,
            BACKGROUND_SIZE,
            BACKGROUND_REPEAT,
            BACKGROUND_ORIGIN,
            BACKGROUND_CLIP,
            BACKGROUND_ATTACHMENT,
            BACKGROUND_POSITION_OR_SIZE,
            BACKGROUND_ORIGIN_OR_CLIP,
            UNDEFINED
        }
    }
}
