2016-01-08
---
 * Database server and web server have been moved back to Sydney, for a better loading performance.
 * Found the major reason that causes slow initial load : IIS which by default recyles after 20 minutes idle. Now this has been `disabled`.

2016-01-09
---
 * Note : For security reasion, `Web.config` is not tracked by git. 
