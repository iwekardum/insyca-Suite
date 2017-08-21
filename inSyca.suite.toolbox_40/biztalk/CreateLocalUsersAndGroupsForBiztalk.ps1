$Computer = $env:COMPUTERNAME
$ADSI = [ADSI]("WinNT://$Computer")

$User = $ADSI.Create("User", "svc_biz.ruleengine")
$User.SetPassword("qQmF8eJTGxhvhhlgJ3Qb")
$User.SetInfo()
$User.FullName = "Rule Engine Update Service"
$User.SetInfo()
$User.UserFlags.Value = 64 + 65536 # ADS_UF_PASSWD_CANT_CHANGE + ADS_UF_DONT_EXPIRE_PASSWD
$User.SetInfo()

$User = $ADSI.Create("User", "svc_biz.sso")
$User.SetPassword("qQmF8eJTGxhvhhlgJ3Qb")
$User.SetInfo()
$User.FullName = "Enterprise Single Sign-On Service"
$User.SetInfo()
$User.UserFlags.Value = 64 + 65536 # ADS_UF_PASSWD_CANT_CHANGE + ADS_UF_DONT_EXPIRE_PASSWD
$User.SetInfo()

$Group = $ADSI.Create('Group', 'grp_biz_sso.admins')
$Group.SetInfo()
$Group.Description  = 'The SSO Administrators group created for Enterprise Single Sign-On.'
$Group.SetInfo()

$Group.Add(("WinNT://$Computer/" + $User.Name))

$Group = $ADSI.Create('Group', 'grp_biz_ssoaff.admins')
$Group.SetInfo()
$Group.Description  = 'The SSO Affiliate Administrators group created for Enterprise Single Sign-On.'
$Group.SetInfo()

$Group = $ADSI.Create('Group', 'grp_biz.admins')
$Group.SetInfo()
$Group.Description  = 'The SSO Affiliate Administrators group created for Enterprise Single Sign-On.'
$Group.SetInfo()

$Group = $ADSI.Create('Group', 'grp_biz.ops')
$Group.SetInfo()
$Group.Description  = 'The BizTalk Server Operators Group has the least privileges necessary to perform tasks required for operating the BizTalk Server environment after installation.'
$Group.SetInfo()

$Group = $ADSI.Create('Group', 'grp_biz_host.users')
$Group.SetInfo()
$Group.Description  = 'Group for accounts with access to the In-Process BizTalk hosts (hosts processes in the BizTalk Server).'
$Group.SetInfo()

$User = $ADSI.Create("User", "svc_biz.host")
$User.SetPassword("qQmF8eJTGxhvhhlgJ3Qb")
$User.SetInfo()
$User.FullName = "BizTalk Host Instance Account"
$User.SetInfo()
$User.UserFlags.Value = 64 + 65536 # ADS_UF_PASSWD_CANT_CHANGE + ADS_UF_DONT_EXPIRE_PASSWD
$User.SetInfo()

$Group.Add(("WinNT://$Computer/" + $User.Name))

$Group = $ADSI.Create('Group', 'grp_biz_isohost.users')
$Group.SetInfo()
$Group.Description  = 'Group for accounts with access to the Isolated BizTalk hosts (hosts processes not running on BizTalk Server, such as HTTP and SOAP)'
$Group.SetInfo()

$User = $ADSI.Create("User", "svc_biz.isohost")
$User.SetPassword("qQmF8eJTGxhvhhlgJ3Qb")
$User.SetInfo()
$User.FullName = "BizTalk Isolated Host Instance Account"
$User.SetInfo()
$User.UserFlags.Value = 64 + 65536 # ADS_UF_PASSWD_CANT_CHANGE + ADS_UF_DONT_EXPIRE_PASSWD
$User.SetInfo()

$Group.Add(("WinNT://$Computer/" + $User.Name))

$Group = $ADSI.Create('Group', 'grp_biz_b2b.ops')
$Group.SetInfo()
$Group.Description  = 'The BizTalk Server B2B Operators Group has the least privileges necessary to perform tasks required for operating the BizTalk Server B2B environment after installation.'
$Group.SetInfo()

$User = $ADSI.Create("User", "svc_monitoring")
$User.SetPassword("qQmF8eJTGxhvhhlgJ3Qb")
$User.SetInfo()
$User.FullName = "inSyca Monitoring Service"
$User.SetInfo()
$User.UserFlags.Value = 64 + 65536 # ADS_UF_PASSWD_CANT_CHANGE + ADS_UF_DONT_EXPIRE_PASSWD
$User.SetInfo()

$Group = $ADSI.Children.Find('Administrators', 'group')
$Group.Add(("WinNT://$Computer/" + $User.Name))

$Group = $ADSI.Children.Find('grp_biz.admins', 'group')
$Group.Add(("WinNT://$Computer/" + $User.Name))

$Group = $ADSI.Children.Find('grp_biz_sso.admins', 'group')
$Group.Add(("WinNT://$Computer/" + $User.Name))