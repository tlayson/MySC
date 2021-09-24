using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class TermsOfUse : USCPageBase
	{
		protected TermsOfUse()
		{
			_PAGENAME = "TermOfUse";
			_USERTYPE = "Pre-Login";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if( IsUserLoggedIn( true ) )
			{
				LoadTerms();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}

		private void LoadTerms()
		{
			StringBuilder sbTOU = new StringBuilder();

			sbTOU.AppendLine("TERMS and AGREEMENT");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("This Statement of Rights and Responsibilities ('Statement', 'Terms', or 'SRR') derives from the MySportsConnect.com Principles, and is our terms of service that governs our relationship with users and others who interact with MySportsConnect.com.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1.Privacy");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("We are a family friendly site and your privacy is very important to us as we continue to have children that have access to the site. We designed our Data Use Policy to make important disclosures about how you can use MySportsConnect.com to share with others and how we collect and can use your content and information.  We encourage you to read the Data Use Policy, and to use it to help you make informed decisions.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("2.Sharing Your Content and Information");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. You own all of the content and information you post on MySportsConnect.com and you can control how it is shared through your privacy and application settings. In addition: 1. For content that is covered by intellectual property rights, like photos and videos (IP content), you specifically give us the following permission, subject to your privacy and application settings: you grant us a non-exclusive, transferable, sub-licensable, royalty-free, worldwide license to use any IP content that you post on or in connection with MySportsConnect.com (IP License). This IP License ends when you delete your IP content or your account unless your content has been shared with others, and they have not deleted it.");
			sbTOU.AppendLine("2. When you delete IP content, it is deleted in a manner similar to emptying the recycle bin on a computer. However, you understand that removed content may persist in backup copies for a reasonable period of time (but will not be available to others).");
			sbTOU.AppendLine("3. When you use an application, the application may ask for your permission to access your content and information as well as content and information that others have shared with you.  We require applications to respect your privacy, and your agreement with that application will control how the application can use, store, and transfer that content and information.  (To learn more about Platform, including how you can control what information other people may share with applications, read our Data Use Policy and Platform Page.)");
			sbTOU.AppendLine("4. When you publish content or information using the Public setting, it means that you are allowing everyone, including people off of MySportsConnect.com, to access and use that information, and to associate it with you (i.e., your name and profile picture).");
			sbTOU.AppendLine("5. We always appreciate your feedback or other suggestions about MySportsConnect.com, but you understand that we may use them without any obligation to compensate you for them (just as you have no obligation to offer them).");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("3.Safety");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. We do our best to keep MySportsConnect.com safe, but we cannot guarantee it. We need your help to keep MySportsConnect.com safe, which includes the following commitments by you: 1. You will not post unauthorized commercial communications (such as spam) on MySportsConnect.com.");
			sbTOU.AppendLine("2. You will not collect users' content or information, or otherwise access MySportsConnect.com, using automated means (such as harvesting bots, robots, spiders, or scrapers) without our prior permission.");
			sbTOU.AppendLine("3. You will not engage in unlawful multi-level marketing, such as a pyramid scheme, on MySportsConnect.com.");
			sbTOU.AppendLine("4. You will not upload viruses or other malicious code.");
			sbTOU.AppendLine("5. You will not solicit login information or access an account belonging to someone else.");
			sbTOU.AppendLine("6. You will not bully, intimidate, or harass any user.");
			sbTOU.AppendLine("7. You will not post content that: is hate speech, threatening, or pornographic; incites violence; or contains nudity or graphic or gratuitous violence.");
			sbTOU.AppendLine("8. You will not develop or operate a third-party application containing alcohol-related, dating or other mature content (including advertisements) without appropriate age-based restrictions.");
			sbTOU.AppendLine("9. You will follow our Promotions Guidelines and all applicable laws if you publicize or offer any contest, giveaway, or sweepstakes (“promotion”) on MySportsConnect.com.");
			sbTOU.AppendLine("10. You will not use MySportsConnect.com to do anything unlawful, misleading, malicious, or discriminatory.");
			sbTOU.AppendLine("11. You will not do anything that could disable, overburden, or impair the proper working or appearance of MySportsConnect.com, such as a denial of service attack or interference with page rendering or other MySportsConnect.com functionality.");
			sbTOU.AppendLine("12. You will not facilitate or encourage any violations of this Statement or our policies.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("4.Registration and Account Security");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. MySportsConnect.com users provide their real names and information, and we need your help to keep it that way. Here are some commitments you make to us relating to registering and maintaining the security of your account: 1. You will not provide any false personal information on MySportsConnect.com, or create an account for anyone other than yourself without permission.");
			sbTOU.AppendLine("2. You will not create more than one personal account.");
			sbTOU.AppendLine("3. If we disable your account, you will not create another one without our permission.");
			sbTOU.AppendLine("4. You will not use your personal timeline for your own commercial gain (such as selling your status update to an advertiser).");
			sbTOU.AppendLine("5. You will not use MySportsConnect.com if you are under 10.");
			sbTOU.AppendLine("6. You will not use MySportsConnect.com if you are a convicted sex offender.");
			sbTOU.AppendLine("7. You will keep your contact information accurate and up-to-date.");
			sbTOU.AppendLine("8. You will not share your password (or in the case of developers, your secret key), let anyone else access your account, or do anything else that might jeopardize the security of your account.");
			sbTOU.AppendLine("9. You will not transfer your account (including any Page or application you administer) to anyone without first getting our written permission.");
			sbTOU.AppendLine("10. If you select a username or similar identifier for your account or Page, we reserve the right to remove or reclaim it if we believe it is appropriate (such as when a trademark owner complains about a username that does not closely relate to a user's actual name).");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("5.Protecting Other People's Rights");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. We respect other people's rights, and expect you to do the same. 1. You will not post content or take any action on MySportsConnect.com that infringes or violates someone else's rights or otherwise violates the law.");
			sbTOU.AppendLine("2. We can remove any content or information you post on MySportsConnect.com if we believe that it violates this Statement or our policies.");
			sbTOU.AppendLine("3. We provide you with tools to help you protect your intellectual property rights. To learn more, visit our How to Report Claims of Intellectual Property Infringement page.");
			sbTOU.AppendLine("4. If we remove your content for infringing someone else's copyright, and you believe we removed it by mistake, we will provide you with an opportunity to appeal.");
			sbTOU.AppendLine("5. If you repeatedly infringe other people's intellectual property rights, we will disable your account when appropriate.");
			sbTOU.AppendLine("6. You will not use our copyrights or trademarks (including MySportsConnect.com, Logos, or any confusingly similar marks, except as expressly permitted by our Brand Usage Guidelines or with our prior written permission.");
			sbTOU.AppendLine("7. If you collect information from users, you will: obtain their consent, make it clear you (and not MySportsConnect.com) are the one collecting their information, and post a privacy policy explaining what information you collect and how you will use it.");
			sbTOU.AppendLine("8. You will not post anyone's identification documents or sensitive financial information on MySportsConnect.com.");
			sbTOU.AppendLine("9. You will not tag users or send email invitations to non-users without their consent. MySportsConnect.com offers social reporting tools to enable users to provide feedback about tagging.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("6. Mobile and Other Devices");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. We currently are in the process to provide our mobile services for free, but please be aware that your carrier's normal rates and fees, such as text messaging fees, will still apply.");
			sbTOU.AppendLine("2. In the event you change or deactivate your mobile telephone number, you will update your account information on MySportsConnect.com within 48 hours to ensure that your messages are not sent to the person who acquires your old number.");
			sbTOU.AppendLine("3. You provide consent and all rights necessary to enable users to sync (including through an application) their devices with any information that is visible to them on MySportsConnect.com.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("7. Special Provisions Applicable to Developers/Operators of Applications and Websites ");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. If you are a developer or operator of a Platform application or website, the following additional terms apply to you: 1. You are responsible for your application and its content and all uses you make of Platform. This includes ensuring your application or use of Platform meets our MySportsConnect.com Platform Policies and our Advertising Guidelines.");
			sbTOU.AppendLine("2. Your access to and use of data you receive from MySportsConnect.com, will be limited as follows: 1. You will only request data you need to operate your application.");
			sbTOU.AppendLine("3. You will have a privacy policy that tells users what user data you are going to use and how you will use, display, share, or transfer that data and you will include your privacy policy URL in the Developer Application.");
			sbTOU.AppendLine("4. You will not use, display, share, or transfer a user’s data in a manner inconsistent with your privacy policy.");
			sbTOU.AppendLine("5. You will delete all data you receive from us concerning a user if the user asks you to do so, and will provide a mechanism for users to make such a request.");
			sbTOU.AppendLine("6. You will not include data you receive from us concerning a user in any advertising creative.");
			sbTOU.AppendLine("7. You will not directly or indirectly transfer any data you receive from us to (or use such data in connection with) any ad network, ad exchange, data broker, or other advertising related toolset, even if a user consents to that transfer or use.");
			sbTOU.AppendLine("8. You will not sell user data.  If you are acquired by or merge with a third party, you can continue to use user data within your application, but you cannot transfer user data outside of your application. ");
			sbTOU.AppendLine("9. We can require you to delete user data if you use it in a way that we determine is inconsistent with users’ expectations.");
			sbTOU.AppendLine("10. We can limit your access to data.");
			sbTOU.AppendLine("11. You will not give us information that you independently collect from a user or a user's content without that user's consent.");
			sbTOU.AppendLine("12. You will make it easy for users to remove or disconnect from your application.");
			sbTOU.AppendLine("13. You will make it easy for users to contact you. We can also share your email address with users and others claiming that you have infringed or otherwise violated their rights.");
			sbTOU.AppendLine("14. You will provide customer support for your application.");
			sbTOU.AppendLine("15. You will not show third party ads or web search boxes on www.mysportsconnect.net");
			sbTOU.AppendLine("16. We give you all rights necessary to use the code, APIs, data, and tools you receive from us.");
			sbTOU.AppendLine("17. You will not sell, transfer, or sublicense our code, APIs, or tools to anyone.");
			sbTOU.AppendLine("18. You will not misrepresent your relationship with MySportsConnect.com to others.");
			sbTOU.AppendLine("19. You may use the logos we make available to developers or issue a press release or other public statement so long as you follow our MySportsConnect.com Platform Policies.");
			sbTOU.AppendLine("20. We can issue a press release describing our relationship with you.");
			sbTOU.AppendLine("21. You will comply with all applicable laws. In particular you will (if applicable): 1. have a policy for removing infringing content and terminating repeat infringers that complies with the Digital Millennium Copyright Act.");
			sbTOU.AppendLine("22. comply with the Video Privacy Protection Act (VPPA), and obtain any opt-in consent necessary from users so that user data subject to the VPPA may be shared on MySportsConnect.com.  You represent that any disclosure to us will not be incidental to the ordinary course of your business.");
			sbTOU.AppendLine("23. We do not guarantee that Platform will always be free.");
			sbTOU.AppendLine("24. You give us all rights necessary to enable your application to work with MySportsConnect.com, including the right to incorporate content and information you provide to us into streams, timelines, and user action stories.");
			sbTOU.AppendLine("25. You give us the right to link to or frame your application, and place content, including ads, around your application.");
			sbTOU.AppendLine("26. We can analyze your application, content, and data for any purpose, including commercial (such as for targeting the delivery of advertisements and indexing content for search).");
			sbTOU.AppendLine("27. To ensure your application is safe for users, we can audit it.");
			sbTOU.AppendLine("28. We can create applications that offer similar features and services to, or otherwise compete with, your application.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("8. About Advertisements and Other Commercial Content Served or Enhanced by MySportsConnect.com");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. Our goal is to deliver ads and commercial content that are valuable to our users and advertisers. In order to help us do that, you agree to the following: 1. You can use your privacy settings to limit how your name and profile picture may be associated with commercial, sponsored, or related content (such as a brand you like) served or enhanced by us. You give us permission to use your name and profile picture in connection with that content, subject to the limits you place.");
			sbTOU.AppendLine("2. We do not give your content or information to advertisers without your consent.");
			sbTOU.AppendLine("3. You understand that we may not always identify paid services and communications as such.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("9 .Special Provisions Applicable to Advertisers ");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. You can target your desired audience by buying ads on MySportsConnect.com or our publisher network. The following additional terms apply to you if you place an order through our online advertising portal (Order): 1. When you place an Order, you will tell us the type of advertising you want to buy, the amount you want to spend, and your bid. If we accept your Order, we will deliver your ads as inventory becomes available. When serving your ad, we do our best to deliver the ads to the audience you specify, although we cannot guarantee in every instance that your ad will reach its intended target.");
			sbTOU.AppendLine("2. In instances where we believe doing so will enhance the effectiveness of your advertising campaign, we may broaden the targeting criteria you specify.");
			sbTOU.AppendLine("3. You will pay for your Orders in accordance with our Payments Terms. The amount you owe will be calculated based on our tracking mechanisms.");
			sbTOU.AppendLine("4. Your ads will comply with our Advertising Guidelines.");
			sbTOU.AppendLine("5. We will determine the size, placement, and positioning of your ads.");
			sbTOU.AppendLine("6. We do not guarantee the activity that your ads will receive, such as the number of clicks your ads will get.");
			sbTOU.AppendLine("7. We cannot control how clicks are generated on your ads. We have systems that attempt to detect and filter certain click activity, but we are not responsible for click fraud, technological issues, or other potentially invalid click activity that may affect the cost of running ads.");
			sbTOU.AppendLine("8. You can cancel your Order at any time through our online portal, but it may take up to 24 hours before the ad stops running.  You are responsible for paying for all ads that run.");
			sbTOU.AppendLine("9. Our license to run your ad will end when we have completed your Order. You understand, however, that if users have interacted with your ad, your ad may remain until the users delete it.");
			sbTOU.AppendLine("10. We can use your ads and related content and information for marketing or promotional purposes.");
			sbTOU.AppendLine("11. You will not issue any press release or make public statements about your relationship with MySportsConnect.com without our prior written permission.");
			sbTOU.AppendLine("12. We may reject or remove any ad for any reason.");
			sbTOU.AppendLine("13. If you are placing ads on someone else's behalf, you must have permission to place those ads, including the following:");
			sbTOU.AppendLine("	1. You warrant that you have the legal authority to bind the advertiser to this Statement.");
			sbTOU.AppendLine("	2. You agree that if the advertiser you represent violates this Statement, we may hold you responsible for that violation.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("10. Special Provisions Applicable to Pages");
			sbTOU.AppendLine("");
			sbTOU.AppendLine(" If you create or administer a Page on MySportsConnect.com, you agree to our Pages Terms.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("11. Special Provisions Applicable to Software");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. If you download our software, such as a stand-alone software product or a browser plugin, you agree that from time to time, the software may download upgrades, updates and additional features from us in order to improve, enhance and further develop the software.");
			sbTOU.AppendLine("2. You will not modify, create derivative works of, decompile or otherwise attempt to extract source code from us, unless you are expressly permitted to do so under an open source license or we give you express written permission.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("12. Amendments");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. We can change this Statement if we provide you notice (by posting the change on the MySportsConnect.com Site Governance Page) and an opportunity to comment.  To get notice of any future changes to this Statement, visit our MySportsConnect.com Site Governance Page and 'like' the Page.");
			sbTOU.AppendLine("2. For changes to sections 7, 8, 9, and 11 (sections relating to payments, application developers, website operators, and advertisers), we will give you a minimum of three days notice. For all other changes we will give you a minimum of seven days notice. Comments to proposed changes will be made on the MySportsConnect.com Site Governance Page.");
			sbTOU.AppendLine("3. If more than 7,000 users post a substantive comment on a particular proposed change, we will also give you the opportunity to participate in a vote in which you will be provided alternatives. The vote shall be binding on us if more than 30% of all active registered users as of the date of the notice vote.");
			sbTOU.AppendLine("4. If we make changes to policies referenced in or incorporated by this Statement, we may provide notice on the Site Governance Page.");
			sbTOU.AppendLine("5. We can make changes for legal or administrative reasons, or to correct an inaccurate statement, upon notice without opportunity to comment.");
			sbTOU.AppendLine("6. Your continued use of MySportsConnect.com following changes to our terms constitutes your acceptance of our amended terms.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("13. Termination");
			sbTOU.AppendLine("");
			sbTOU.AppendLine(" If you violate the letter or spirit of this Statement, or otherwise create risk or possible legal exposure for us, we can stop providing all or part of MySportsConnect.com to you. We will notify you by email or at the next time you attempt to access your account. You may also delete your account or disable your application at any time. ");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("14. Disputes");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. You will resolve any claim, cause of action or dispute (claim) you have with us arising out of or relating to this Statement or MySportsConnect.com exclusively in a state or federal court located in King County. The laws of the State of Washington will govern this Statement, as well as any claim that might arise between you and us, without regard to conflict of law provisions. You agree to submit to the personal jurisdiction of the courts located in King County, Washington for the purpose of litigating all such claims.");
			sbTOU.AppendLine("2. If anyone brings a claim against us related to your actions, content or information on MySportsConnect.com, you will indemnify and hold us harmless from and against all damages, losses, and expenses of any kind (including reasonable legal fees and costs) related to such claim. Although we provide rules for user conduct, we do not control or direct users' actions on MySportsConnect.com and are not responsible for the content or information users transmit or share on MySportsConnect.com. We are not responsible for any offensive, inappropriate, obscene, unlawful or otherwise objectionable content or information you may encounter on MySportsConnect.com. We are not responsible for the conduct, whether online or offline, or any user of MySportsConnect.com. ");
			sbTOU.AppendLine("3. WE TRY TO KEEP MySportsConnect.COM UP, BUG-FREE, AND SAFE, BUT YOU USE IT AT YOUR OWN RISK. WE ARE PROVIDING MySportsConnect.COM AS IS WITHOUT ANY EXPRESS OR IMPLIED WARRANTIES INCLUDING, BUT NOT LIMITED TO, IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, AND NON-INFRINGEMENT. WE DO NOT GUARANTEE THAT MySportsConnect.COM WILL ALWAYS BE SAFE, SECURE OR ERROR-FREE OR THAT MySportsConnect.COM WILL ALWAYS FUNCTION WITHOUT DISRUPTIONS, DELAYS OR IMPERFECTIONS. MySportsConnect.COM IS NOT RESPONSIBLE FOR THE ACTIONS, CONTENT, INFORMATION, OR DATA OF THIRD PARTIES, AND YOU RELEASE US, OUR DIRECTORS, OFFICERS, EMPLOYEES, AND AGENTS FROM ANY CLAIMS AND DAMAGES, KNOWN AND UNKNOWN.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("15. Special Provisions Applicable to Users Outside the United States");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. We strive to create a global community with consistent standards for everyone, but we also strive to respect local laws. The following provisions apply to users and non-users who interact with MySportsConnect.com outside the United States: 1. You consent to having your personal data transferred to and processed in the United States.");
			sbTOU.AppendLine("2. If you are located in a country embargoed by the United States, or are on the U.S. Treasury Department's list of Specially Designated Nationals you will not engage in commercial activities on MySportsConnect.com (such as advertising or payments) or operate a Platform application or website.");
			sbTOU.AppendLine("3. Certain specific terms that apply only for German users are available here. ");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("16. Definitions");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. By 'MySportsConnect' we mean the features and services we make available, including through (a) our website at www.mysportsconnect.net and any other MySportsConnect.com branded or co-branded websites (including sub-domains, international versions, widgets, and mobile versions); (b) our Platform; (c) social plugins such as the Like button, the Share button and other similar offerings and (d) other media, software (such as a toolbar), devices, or networks now existing or later developed.");
			sbTOU.AppendLine("2. By 'Platform' we mean a set of APIs and services (such as content) that enable others, including application developers and website operators, to retrieve data from MySportsConnect.com or provide data to us.");
			sbTOU.AppendLine("3. By 'information' we mean facts and other information about you, including actions taken by users and non-users who interact with MySportsConnect.com.");
			sbTOU.AppendLine("4. By 'content' we mean anything you or other users post on MySportsConnect.com that would not be included in the definition of information.");
			sbTOU.AppendLine("5. By 'data' or 'user data' or 'user's data' we mean any data, including a user's content or information that you or third parties can retrieve from MySportsConnect.com or provide to MySportsConnect.com through Platform.");
			sbTOU.AppendLine("6. By 'use' we mean use, copy, publicly perform or display, distribute, modify, translate, and create derivative works of.");
			sbTOU.AppendLine("7. By 'active registered user' we mean a user who has logged into MySportsConnect.com at least once in the previous 30 days.");
			sbTOU.AppendLine("8. By 'application' we mean any application or website that uses or accesses Platform, as well as anything else that receives or has received data from us.  If you no longer access Platform but have not deleted all data from us, the term application will apply until you delete the data.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("17. Other");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("1. If you are a resident of or have your principal place of business in the US or Canada, this Statement is an agreement between you and MySportsConnect.com, Inc.  Otherwise, this Statement is an agreement between you and MySportsConnect.com.");
			sbTOU.AppendLine("2. This Statement makes up the entire agreement between the parties regarding MySportsConnect.com, and supersedes any prior agreements.");
			sbTOU.AppendLine("3. If any portion of this Statement is found to be unenforceable, the remaining portion will remain in full force and effect.");
			sbTOU.AppendLine("4. If we fail to enforce any of this Statement, it will not be considered a waiver.");
			sbTOU.AppendLine("5. Any amendment to or waiver of this Statement must be made in writing and signed by us.");
			sbTOU.AppendLine("6. You will not transfer any of your rights or obligations under this Statement to anyone else without our consent.");
			sbTOU.AppendLine("7. All of our rights and obligations under this Statement are freely assignable by us in connection with a merger, acquisition, or sale of assets, or by operation of law or otherwise.");
			sbTOU.AppendLine("8. Nothing in this Statement shall prevent us from complying with the law.");
			sbTOU.AppendLine("9. This Statement does not confer any third party beneficiary rights.");
			sbTOU.AppendLine("10. We reserve all rights not expressly granted to you.");
			sbTOU.AppendLine("11. You will comply with all applicable laws when using or accessing MySportsConnect.com.");
			sbTOU.AppendLine("");
			sbTOU.AppendLine("MySportsConnect.com © 2012 • English (US)");

			txtTermsOfUse.Text = sbTOU.ToString();
		}

		protected void OnClickButtonAccept(object sender, ImageClickEventArgs e)
		{
			UserAccount acct = GetActiveUser();
			if( null != acct )
			{
				acct.AcceptedTOU = true;
				acct.UpdateTOU();
				RedirectToDefault( acct );
			}
		}

		protected void OnClickButtonDecline(object sender, ImageClickEventArgs e)
		{
			DeleteSessionVariables();
			RedirectToLoginPage();
		}
	}
}