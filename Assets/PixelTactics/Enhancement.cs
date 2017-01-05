/*************************************************************************
 * 
 * FELICITY CONFIDENTIAL
 * __________________
 * 
 *  [2016] - [2016] Felicity Entertainment
 *  All Rights Reserved.
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Felicity Entertainment and its suppliers,
 * if any.  The intellectual and technical concepts contained
 * herein are proprietary to Felicity Entertainment and its 
 * suppliers and may be covered by U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or 
 * copyright law. Dissemination of this information or reproduction
 * of this material is strictly forbidden unless prior written
 * permission is obtained from Felicity Entertainment.
 */

/*
* Enhancement.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing the effects modifying a Character
	/// </summary>
	public class Enhancement
	{

		//----------------------------------------------------------------

		/// <summary>
		/// The description of the Enhancement
		/// </summary>
		public string Description;

		/// <summary>
		/// The source of the Enhancement
		/// </summary>
		public string Source;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.Enhancement"/> class.
		/// </summary>
		/// <param name="description">Description.</param>
		/// <param name="source">Source.</param>
		public Enhancement (string description, string source)
		{
			this.Description = description;
			this.Source = source;
		}

		//----------------------------------------------------------------

	} // End Enhancement class

} // End namespace

