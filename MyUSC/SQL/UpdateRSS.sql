use MyUSC;

/* RSS Correction */
update RSSFeeds set Description='BMX' WHERE RSSID >= 814 AND RSSID <= 821
update RSSFeeds set Description='Diving' WHERE RSSID >= 822 AND RSSID <= 825
update RSSFeeds set Description='Hunting' WHERE RSSID=826
update RSSFeeds set Description='NCAA Football' WHERE RSSID >= 888 AND RSSID <= 899
update RSSFeeds set Name='University of Connecticut' WHERE RSSID=895
update RSSFeeds set Name='South Florida Bulls' WHERE RSSID=897
update RSSFeeds set Name='Wake Forest Deacon Demons' WHERE RSSID=885
update RSSFeeds set Description='Pro Golf' WHERE RSSID=186
update RSSFeeds set Description='NCAA Baseball' WHERE RSSID=191
update RSSFeeds set Description='Youth Sports' WHERE RSSID=350
update RSSFeeds set Description='Youth Sports' WHERE RSSID=351
update RSSFeeds set Description='Youth Sports' WHERE RSSID=352
update RSSFeeds set Description='Youth Sports' WHERE RSSID=353
update RSSFeeds set Description='Youth Sports' WHERE RSSID=354
update RSSFeeds set Description='Youth Sports' WHERE RSSID=355


Insert into RSSFeeds (Name,URL,[Description],CreationUser, CreationDate,LastUpdateUserTime)
values ('Extreme Sports Front Page','http://extremesportsx.com/feed/','Extreme Sports','tlayson',GETDATE(),'');

/* Names */
update SportName SET RSSID=906 WHERE NameKeyID=17
update SportName SET RSSID=194 WHERE NameKeyID=11
update SportName SET RSSID=161 WHERE NameKeyID=7
update SportName SET RSSID=188 WHERE NameKeyID=8
update SportName SET RSSID=187 WHERE NameKeyID=9
update SportName SET RSSID=189 WHERE NameKeyID=10
update SportName SET RSSID=341 WHERE NameKeyID=13
update SportName SET RSSID=0 WHERE NameKeyID=14
update SportName SET RSSID=362 WHERE NameKeyID=15
update SportName SET RSSID=191 WHERE NameKeyID=16
update SportName SET RSSID=827 WHERE NameKeyID=18

update SportName SET Description='National Basketball Association' WHERE NameKeyID=2
update SportName SET Description='Womens National Basketball Association' WHERE NameKeyID=6

/* Types */
update SportType SET RSSID=858 WHERE TypeKeyID=1
update SportType SET RSSID=859 WHERE TypeKeyID=2
update SportType SET RSSID=842 WHERE TypeKeyID=3
update SportType SET RSSID=847 WHERE TypeKeyID=4
update SportType SET RSSID=866 WHERE TypeKeyID=5
update SportType SET RSSID=867 WHERE TypeKeyID=6
update SportType SET RSSID=878 WHERE TypeKeyID=7
update SportType SET RSSID=879 WHERE TypeKeyID=8
update SportType SET RSSID=876 WHERE TypeKeyID=22
update SportType SET RSSID=877 WHERE TypeKeyID=23
update SportType SET RSSID=868 WHERE TypeKeyID=24
update SportType SET RSSID=869 WHERE TypeKeyID=25
update SportType SET RSSID=194 WHERE TypeKeyID=26
update SportType SET RSSID=194 WHERE TypeKeyID=27
update SportType SET RSSID=194 WHERE TypeKeyID=28
update SportType SET RSSID=194 WHERE TypeKeyID=29
update SportType SET RSSID=880 WHERE TypeKeyID=30
update SportType SET RSSID=881 WHERE TypeKeyID=31
update SportType SET RSSID=905 WHERE TypeKeyID=37
update SportType SET RSSID=0 WHERE TypeKeyID=38
update SportType SET RSSID=0 WHERE TypeKeyID=39
update SportType SET RSSID=0 WHERE TypeKeyID=40
update SportType SET RSSID=745 WHERE TypeKeyID=41
update SportType SET RSSID=813 WHERE TypeKeyID=46
update SportType SET RSSID=821 WHERE TypeKeyID=47
update SportType SET RSSID=0 WHERE TypeKeyID=49
update SportType SET RSSID=901 WHERE TypeKeyID=50
update SportType SET Description='Womens National Basketball Association' WHERE TypeKeyID=7
update SportType SET Description='Womens National Basketball Association' WHERE TypeKeyID=8


/* Divisions */
update SportDivision SET RSSID=852 WHERE DivKeyID=1
update SportDivision SET RSSID=853 WHERE DivKeyID=2
update SportDivision SET RSSID=854 WHERE DivKeyID=3
update SportDivision SET RSSID=855 WHERE DivKeyID=4
update SportDivision SET RSSID=856 WHERE DivKeyID=5
update SportDivision SET RSSID=857 WHERE DivKeyID=6
update SportDivision SET RSSID=843 WHERE DivKeyID=13
update SportDivision SET RSSID=844 WHERE DivKeyID=14
update SportDivision SET RSSID=845 WHERE DivKeyID=15
update SportDivision SET RSSID=846 WHERE DivKeyID=16
update SportDivision SET RSSID=848 WHERE DivKeyID=17
update SportDivision SET RSSID=849 WHERE DivKeyID=18
update SportDivision SET RSSID=850 WHERE DivKeyID=19
update SportDivision SET RSSID=851 WHERE DivKeyID=20
update SportDivision SET RSSID=195 WHERE DivKeyID=27
update SportDivision SET RSSID=196 WHERE DivKeyID=28
update SportDivision SET RSSID=197 WHERE DivKeyID=29
update SportDivision SET RSSID=198 WHERE DivKeyID=30
update SportDivision SET RSSID=199 WHERE DivKeyID=31
update SportDivision SET RSSID=200 WHERE DivKeyID=35
update SportDivision SET RSSID=201 WHERE DivKeyID=37
update SportDivision SET RSSID=202 WHERE DivKeyID=38
update SportDivision SET RSSID=257 WHERE DivKeyID=40
update SportDivision SET RSSID=274 WHERE DivKeyID=44
update SportDivision SET RSSID=306 WHERE DivKeyID=48
update SportDivision SET RSSID=325 WHERE DivKeyID=51
update SportDivision SET RSSID=332 WHERE DivKeyID=52
update SportDivision SET RSSID=338 WHERE DivKeyID=53
update SportDivision SET RSSID=194 WHERE DivKeyID=68
update SportDivision SET RSSID=70 WHERE DivKeyID=117
update SportDivision SET RSSID=350 WHERE DivKeyID=143
update SportDivision SET RSSID=351 WHERE DivKeyID=146
update SportDivision SET RSSID=352 WHERE DivKeyID=147
update SportDivision SET RSSID=353 WHERE DivKeyID=150
update SportDivision SET RSSID=354 WHERE DivKeyID=151
update SportDivision SET RSSID=355 WHERE DivKeyID=155
update SportDivision SET RSSID=356 WHERE DivKeyID=156
update SportDivision SET RSSID=357 WHERE DivKeyID=157
update SportDivision SET RSSID=358 WHERE DivKeyID=158
update SportDivision SET RSSID=359 WHERE DivKeyID=159
update SportDivision SET RSSID=360 WHERE DivKeyID=160
update SportDivision SET RSSID=361 WHERE DivKeyID=161
update SportDivision SET RSSID=362 WHERE DivKeyID=162
update SportDivision SET RSSID=363 WHERE DivKeyID=163
update SportDivision SET RSSID=364 WHERE DivKeyID=164
update SportDivision SET RSSID=365 WHERE DivKeyID=165
update SportDivision SET RSSID=366 WHERE DivKeyID=166
update SportDivision SET RSSID=368 WHERE DivKeyID=167
update SportDivision SET RSSID=367 WHERE DivKeyID=168
update SportDivision SET RSSID=371 WHERE DivKeyID=169
update SportDivision SET RSSID=372 WHERE DivKeyID=171
update SportDivision SET RSSID=373 WHERE DivKeyID=173
update SportDivision SET RSSID=374 WHERE DivKeyID=174
update SportDivision SET RSSID=375 WHERE DivKeyID=176
update SportDivision SET RSSID=376 WHERE DivKeyID=177
update SportDivision SET RSSID=377 WHERE DivKeyID=178
update SportDivision SET RSSID=378 WHERE DivKeyID=179
update SportDivision SET RSSID=379 WHERE DivKeyID=182
update SportDivision SET RSSID=380 WHERE DivKeyID=183
update SportDivision SET RSSID=381 WHERE DivKeyID=184
update SportDivision SET RSSID=382 WHERE DivKeyID=186
update SportDivision SET RSSID=383 WHERE DivKeyID=187
update SportDivision SET RSSID=384 WHERE DivKeyID=189
update SportDivision SET RSSID=385 WHERE DivKeyID=190
update SportDivision SET RSSID=386 WHERE DivKeyID=191
update SportDivision SET RSSID=387 WHERE DivKeyID=194
update SportDivision SET RSSID=388 WHERE DivKeyID=195
update SportDivision SET RSSID=389 WHERE DivKeyID=196
update SportDivision SET RSSID=390 WHERE DivKeyID=197
update SportDivision SET RSSID=391 WHERE DivKeyID=198
update SportDivision SET RSSID=392 WHERE DivKeyID=200
update SportDivision SET RSSID=741 WHERE DivKeyID=201
update SportDivision SET RSSID=746 WHERE DivKeyID=202
update SportDivision SET RSSID=748 WHERE DivKeyID=203
update SportDivision SET RSSID=751 WHERE DivKeyID=204
update SportDivision SET RSSID=754 WHERE DivKeyID=205
update SportDivision SET RSSID=756 WHERE DivKeyID=206
update SportDivision SET RSSID=761 WHERE DivKeyID=207
update SportDivision SET RSSID=762 WHERE DivKeyID=208
update SportDivision SET RSSID=763 WHERE DivKeyID=209
update SportDivision SET RSSID=764 WHERE DivKeyID=210
update SportDivision SET RSSID=765 WHERE DivKeyID=211
update SportDivision SET RSSID=766 WHERE DivKeyID=212
update SportDivision SET RSSID=767 WHERE DivKeyID=213
update SportDivision SET RSSID=768 WHERE DivKeyID=214
update SportDivision SET RSSID=769 WHERE DivKeyID=215
update SportDivision SET RSSID=770 WHERE DivKeyID=216
update SportDivision SET RSSID=771 WHERE DivKeyID=217
update SportDivision SET RSSID=772 WHERE DivKeyID=218
update SportDivision SET RSSID=773 WHERE DivKeyID=219
update SportDivision SET RSSID=774 WHERE DivKeyID=220
update SportDivision SET RSSID=775 WHERE DivKeyID=221
update SportDivision SET RSSID=776 WHERE DivKeyID=222
update SportDivision SET RSSID=777 WHERE DivKeyID=223
update SportDivision SET RSSID=778 WHERE DivKeyID=224
update SportDivision SET RSSID=779 WHERE DivKeyID=225
update SportDivision SET RSSID=780 WHERE DivKeyID=226
update SportDivision SET RSSID=781 WHERE DivKeyID=227
update SportDivision SET RSSID=782 WHERE DivKeyID=228
update SportDivision SET RSSID=783 WHERE DivKeyID=229
update SportDivision SET RSSID=784 WHERE DivKeyID=230
update SportDivision SET RSSID=785 WHERE DivKeyID=231
update SportDivision SET RSSID=786 WHERE DivKeyID=232
update SportDivision SET RSSID=787 WHERE DivKeyID=233
update SportDivision SET RSSID=788 WHERE DivKeyID=234
update SportDivision SET RSSID=0 WHERE DivKeyID=235
update SportDivision SET RSSID=0 WHERE DivKeyID=236
update SportDivision SET RSSID=0 WHERE DivKeyID=237
update SportDivision SET RSSID=812 WHERE DivKeyID=238
update SportDivision SET RSSID=814 WHERE DivKeyID=239
update SportDivision SET RSSID=815 WHERE DivKeyID=240
update SportDivision SET RSSID=816 WHERE DivKeyID=241
update SportDivision SET RSSID=817 WHERE DivKeyID=242
update SportDivision SET RSSID=818 WHERE DivKeyID=243
update SportDivision SET RSSID=819 WHERE DivKeyID=244
update SportDivision SET RSSID=820 WHERE DivKeyID=245
update SportDivision SET RSSID=822 WHERE DivKeyID=246
update SportDivision SET RSSID=823 WHERE DivKeyID=247
update SportDivision SET RSSID=824 WHERE DivKeyID=248
update SportDivision SET RSSID=825 WHERE DivKeyID=249
update SportDivision SET RSSID=826 WHERE DivKeyID=250
update SportDivision SET RSSID=828 WHERE DivKeyID=251
update SportDivision SET RSSID=829 WHERE DivKeyID=252
update SportDivision SET RSSID=830 WHERE DivKeyID=253
update SportDivision SET RSSID=831 WHERE DivKeyID=254
update SportDivision SET RSSID=832 WHERE DivKeyID=255
update SportDivision SET RSSID=833 WHERE DivKeyID=256
update SportDivision SET RSSID=834 WHERE DivKeyID=257
update SportDivision SET RSSID=835 WHERE DivKeyID=258
update SportDivision SET RSSID=836 WHERE DivKeyID=259
update SportDivision SET RSSID=837 WHERE DivKeyID=260
update SportDivision SET RSSID=838 WHERE DivKeyID=261
update SportDivision SET RSSID=839 WHERE DivKeyID=262
update SportDivision SET RSSID=840 WHERE DivKeyID=263
update SportDivision SET RSSID=841 WHERE DivKeyID=264
update SportDivision SET RSSID=900 WHERE DivKeyID=265
update SportDivision SET RSSID=902 WHERE DivKeyID=266
update SportDivision SET RSSID=903 WHERE DivKeyID=267
update SportDivision SET RSSID=904 WHERE DivKeyID=268

update SportDivision SET Description='Skatboarding' WHERE Description='Skaeboarding'
update SportDivision SET RSSID=0 WHERE Description Like '%National Hockey League%'
update SportDivision SET RSSID=0 WHERE Description Like '%National Basketball Association%'
update SportDivision SET RSSID=194 WHERE Description Like '%NCAA Football%' AND RSSID=0
update SportDivision SET RSSID=194 WHERE Description Like '%NCAA Football%' AND RSSID=1
update SportDivision SET Description='Womens National Basketball Association' WHERE Description='Womens National Basketball League'

/* Teams */
update SportTeam SET RSSID=888 WHERE TeamKeyID=137
update SportTeam SET RSSID=889 WHERE TeamKeyID=139
update SportTeam SET RSSID=890 WHERE TeamKeyID=141
update SportTeam SET RSSID=891 WHERE TeamKeyID=140
update SportTeam SET RSSID=892 WHERE TeamKeyID=143
update SportTeam SET RSSID=893 WHERE TeamKeyID=144
update SportTeam SET RSSID=894 WHERE TeamKeyID=145
update SportTeam SET RSSID=895 WHERE TeamKeyID=146
update SportTeam SET RSSID=896 WHERE TeamKeyID=147
update SportTeam SET RSSID=897 WHERE TeamKeyID=150
update SportTeam SET RSSID=898 WHERE TeamKeyID=148
update SportTeam SET RSSID=899 WHERE TeamKeyID=206
update SportTeam SET RSSID=742 WHERE TeamKeyID=1648
update SportTeam SET RSSID=744 WHERE TeamKeyID=1650
update SportTeam SET RSSID=743 WHERE TeamKeyID=1649
update SportTeam SET RSSID=747 WHERE TeamKeyID=1651
update SportTeam SET RSSID=749 WHERE TeamKeyID=1652
update SportTeam SET RSSID=750 WHERE TeamKeyID=1653
update SportTeam SET RSSID=752 WHERE TeamKeyID=1654
update SportTeam SET RSSID=753 WHERE TeamKeyID=1655
update SportTeam SET RSSID=755 WHERE TeamKeyID=1656
update SportTeam SET RSSID=757 WHERE TeamKeyID=1657
update SportTeam SET RSSID=758 WHERE TeamKeyID=1658
update SportTeam SET RSSID=759 WHERE TeamKeyID=1659
update SportTeam SET RSSID=760 WHERE TeamKeyID=1660
update SportTeam SET RSSID=789 WHERE TeamKeyID=1661
update SportTeam SET RSSID=790 WHERE TeamKeyID=1662
update SportTeam SET RSSID=791 WHERE TeamKeyID=1663
update SportTeam SET RSSID=792 WHERE TeamKeyID=1664
update SportTeam SET RSSID=793 WHERE TeamKeyID=1665
update SportTeam SET RSSID=794 WHERE TeamKeyID=1666
update SportTeam SET RSSID=795 WHERE TeamKeyID=1667
update SportTeam SET RSSID=796 WHERE TeamKeyID=1668
update SportTeam SET RSSID=797 WHERE TeamKeyID=1669
update SportTeam SET RSSID=798 WHERE TeamKeyID=1670
update SportTeam SET RSSID=799 WHERE TeamKeyID=1671
update SportTeam SET RSSID=800 WHERE TeamKeyID=1672
update SportTeam SET RSSID=801 WHERE TeamKeyID=1673
update SportTeam SET RSSID=802 WHERE TeamKeyID=1674
update SportTeam SET RSSID=803 WHERE TeamKeyID=1675
update SportTeam SET RSSID=804 WHERE TeamKeyID=1676
update SportTeam SET RSSID=805 WHERE TeamKeyID=1677
update SportTeam SET RSSID=806 WHERE TeamKeyID=1678
update SportTeam SET RSSID=807 WHERE TeamKeyID=1679
update SportTeam SET RSSID=808 WHERE TeamKeyID=1680
update SportTeam SET RSSID=809 WHERE TeamKeyID=1681
update SportTeam SET RSSID=810 WHERE TeamKeyID=1682
update SportTeam SET RSSID=811 WHERE TeamKeyID=1683
update SportTeam SET RSSID=203,[Description]='NCAA Football' WHERE TeamKeyID=123

/* NASCAR */
update SportTeam SET RSSID=165 WHERE TeamKeyID=815
update SportTeam SET RSSID=166 WHERE TeamKeyID=819
update SportTeam SET RSSID=166 WHERE TeamKeyID=846
update SportTeam SET RSSID=167 WHERE TeamKeyID=789
update SportTeam SET RSSID=168 WHERE TeamKeyID=822
update SportTeam SET RSSID=168 WHERE TeamKeyID=817
update SportTeam SET RSSID=169 WHERE TeamKeyID=788
update SportTeam SET RSSID=171 WHERE TeamKeyID=791
update SportTeam SET RSSID=172 WHERE TeamKeyID=797
update SportTeam SET RSSID=173 WHERE TeamKeyID=810
update SportTeam SET RSSID=175 WHERE TeamKeyID=781
update SportTeam SET RSSID=176 WHERE TeamKeyID=806
update SportTeam SET RSSID=177 WHERE TeamKeyID=796
update SportTeam SET RSSID=178 WHERE TeamKeyID=799
update SportTeam SET RSSID=178 WHERE TeamKeyID=840
update SportTeam SET RSSID=179 WHERE TeamKeyID=783
update SportTeam SET RSSID=180 WHERE TeamKeyID=792
update SportTeam SET RSSID=181 WHERE TeamKeyID=784
update SportTeam SET RSSID=182 WHERE TeamKeyID=973
update SportTeam SET RSSID=183 WHERE TeamKeyID=805
update SportTeam SET RSSID=184 WHERE TeamKeyID=790
update SportTeam SET RSSID=185 WHERE TeamKeyID=793
update SportTeam SET RSSID=185 WHERE TeamKeyID=831

/* Golf */
update SportTeam SET RSSID=188 WHERE Description Like '%Professional Golfers Association%'

/* Bowling */
update SportTeam set Description='Professional Bowlers Association',RSSID=189 WHERE TeamKeyID >= 1598 AND TeamKeyID <= 1647

/* NCAA Football */
update SportTeam SET RSSID=204 WHERE TeamKeyID=124
update SportTeam SET RSSID=205 WHERE TeamKeyID=129
update SportTeam SET RSSID=206 WHERE TeamKeyID=125
update SportTeam SET RSSID=207 WHERE TeamKeyID=130
update SportTeam SET RSSID=208 WHERE TeamKeyID=131
update SportTeam SET RSSID=209 WHERE TeamKeyID=134
update SportTeam SET RSSID=210 WHERE TeamKeyID=136
update SportTeam SET RSSID=211 WHERE TeamKeyID=138
update SportTeam SET RSSID=212 WHERE TeamKeyID=142
update SportTeam SET RSSID=213 WHERE TeamKeyID=149
update SportTeam SET RSSID=214 WHERE TeamKeyID=158
update SportTeam SET RSSID=215 WHERE TeamKeyID=177
update SportTeam SET RSSID=216 WHERE TeamKeyID=178
update SportTeam SET RSSID=217 WHERE TeamKeyID=181
update SportTeam SET RSSID=218 WHERE TeamKeyID=188
update SportTeam SET RSSID=219 WHERE TeamKeyID=182
update SportTeam SET RSSID=220 WHERE TeamKeyID=183
update SportTeam SET RSSID=221 WHERE TeamKeyID=189
update SportTeam SET RSSID=222 WHERE TeamKeyID=190
update SportTeam SET RSSID=223 WHERE TeamKeyID=184
update SportTeam SET RSSID=224 WHERE TeamKeyID=192
update SportTeam SET RSSID=225 WHERE TeamKeyID=193
update SportTeam SET RSSID=226 WHERE TeamKeyID=194
update SportTeam SET RSSID=227 WHERE TeamKeyID=195
update SportTeam SET RSSID=228 WHERE TeamKeyID=196
update SportTeam SET RSSID=229 WHERE TeamKeyID=208
update SportTeam SET RSSID=230 WHERE TeamKeyID=209
update SportTeam SET RSSID=231 WHERE TeamKeyID=210
update SportTeam SET RSSID=232 WHERE TeamKeyID=203
update SportTeam SET RSSID=233 WHERE TeamKeyID=213
update SportTeam SET RSSID=234 WHERE TeamKeyID=220
update SportTeam SET RSSID=235 WHERE TeamKeyID=221
update SportTeam SET RSSID=236 WHERE TeamKeyID=222
update SportTeam SET RSSID=237 WHERE TeamKeyID=214
update SportTeam SET RSSID=238 WHERE TeamKeyID=223
update SportTeam SET RSSID=239 WHERE TeamKeyID=224
update SportTeam SET RSSID=240 WHERE TeamKeyID=218
update SportTeam SET RSSID=241 WHERE TeamKeyID=226
update SportTeam SET RSSID=242 WHERE TeamKeyID=228
update SportTeam SET RSSID=243 WHERE TeamKeyID=229
update SportTeam SET RSSID=244 WHERE TeamKeyID=230
update SportTeam SET RSSID=245 WHERE TeamKeyID=232
update SportTeam SET RSSID=246 WHERE TeamKeyID=233
update SportTeam SET RSSID=247 WHERE TeamKeyID=234
update SportTeam SET RSSID=248 WHERE TeamKeyID=240
update SportTeam SET RSSID=249 WHERE TeamKeyID=241
update SportTeam SET RSSID=250 WHERE TeamKeyID=244
update SportTeam SET RSSID=251 WHERE TeamKeyID=246
update SportTeam SET RSSID=252 WHERE TeamKeyID=245
update SportTeam SET RSSID=253 WHERE TeamKeyID=247
update SportTeam SET RSSID=254 WHERE TeamKeyID=248
update SportTeam SET RSSID=255 WHERE TeamKeyID=249
update SportTeam SET RSSID=256 WHERE TeamKeyID=251
update SportTeam SET RSSID=258 WHERE TeamKeyID=254
update SportTeam SET RSSID=259 WHERE TeamKeyID=255
update SportTeam SET RSSID=260 WHERE TeamKeyID=256
update SportTeam SET RSSID=261 WHERE TeamKeyID=258
update SportTeam SET RSSID=263 WHERE TeamKeyID=260
update SportTeam SET RSSID=264 WHERE TeamKeyID=261
update SportTeam SET RSSID=265 WHERE TeamKeyID=263
update SportTeam SET RSSID=266 WHERE TeamKeyID=266
update SportTeam SET RSSID=267 WHERE TeamKeyID=267
update SportTeam SET RSSID=268 WHERE TeamKeyID=269
update SportTeam SET RSSID=269 WHERE TeamKeyID=270
update SportTeam SET RSSID=270 WHERE TeamKeyID=271
update SportTeam SET RSSID=271 WHERE TeamKeyID=275
update SportTeam SET RSSID=272 WHERE TeamKeyID=276
update SportTeam SET RSSID=273 WHERE TeamKeyID=277
update SportTeam SET RSSID=275 WHERE TeamKeyID=279
update SportTeam SET RSSID=276 WHERE TeamKeyID=280
update SportTeam SET RSSID=277 WHERE TeamKeyID=281
update SportTeam SET RSSID=278 WHERE TeamKeyID=282
update SportTeam SET RSSID=279 WHERE TeamKeyID=283
update SportTeam SET RSSID=280 WHERE TeamKeyID=284
update SportTeam SET RSSID=281 WHERE TeamKeyID=285
update SportTeam SET RSSID=283 WHERE TeamKeyID=286
update SportTeam SET RSSID=284 WHERE TeamKeyID=287
update SportTeam SET RSSID=285 WHERE TeamKeyID=288
update SportTeam SET RSSID=286 WHERE TeamKeyID=289
update SportTeam SET RSSID=287 WHERE TeamKeyID=291
update SportTeam SET RSSID=288 WHERE TeamKeyID=292
update SportTeam SET RSSID=289 WHERE TeamKeyID=293
update SportTeam SET RSSID=290 WHERE TeamKeyID=294
update SportTeam SET RSSID=291 WHERE TeamKeyID=295
update SportTeam SET RSSID=293 WHERE TeamKeyID=298
update SportTeam SET RSSID=294 WHERE TeamKeyID=300
update SportTeam SET RSSID=295 WHERE TeamKeyID=301
update SportTeam SET RSSID=296 WHERE TeamKeyID=302
update SportTeam SET RSSID=297 WHERE TeamKeyID=304
update SportTeam SET RSSID=299 WHERE TeamKeyID=306
update SportTeam SET RSSID=300 WHERE TeamKeyID=307
update SportTeam SET RSSID=301 WHERE TeamKeyID=308
update SportTeam SET RSSID=302 WHERE TeamKeyID=309
update SportTeam SET RSSID=303 WHERE TeamKeyID=310
update SportTeam SET RSSID=304 WHERE TeamKeyID=311
update SportTeam SET RSSID=305 WHERE TeamKeyID=313
update SportTeam SET RSSID=307 WHERE TeamKeyID=315
update SportTeam SET RSSID=308 WHERE TeamKeyID=316
update SportTeam SET RSSID=309 WHERE TeamKeyID=317
update SportTeam SET RSSID=310 WHERE TeamKeyID=319
update SportTeam SET RSSID=311 WHERE TeamKeyID=320
update SportTeam SET RSSID=312 WHERE TeamKeyID=321
update SportTeam SET RSSID=313 WHERE TeamKeyID=324
update SportTeam SET RSSID=314 WHERE TeamKeyID=325
update SportTeam SET RSSID=315 WHERE TeamKeyID=326
update SportTeam SET RSSID=316 WHERE TeamKeyID=327
update SportTeam SET RSSID=317 WHERE TeamKeyID=330
update SportTeam SET RSSID=318 WHERE TeamKeyID=333
update SportTeam SET RSSID=319 WHERE TeamKeyID=334
update SportTeam SET RSSID=320 WHERE TeamKeyID=335
update SportTeam SET RSSID=321 WHERE TeamKeyID=336
update SportTeam SET RSSID=322 WHERE TeamKeyID=337
update SportTeam SET RSSID=323 WHERE TeamKeyID=338
update SportTeam SET RSSID=324 WHERE TeamKeyID=340
update SportTeam SET RSSID=326 WHERE TeamKeyID=341
update SportTeam SET RSSID=327 WHERE TeamKeyID=343
update SportTeam SET RSSID=328 WHERE TeamKeyID=344
update SportTeam SET RSSID=329 WHERE TeamKeyID=345
update SportTeam SET RSSID=330 WHERE TeamKeyID=346
update SportTeam SET RSSID=331 WHERE TeamKeyID=349
update SportTeam SET RSSID=333 WHERE TeamKeyID=350
update SportTeam SET RSSID=334 WHERE TeamKeyID=352
update SportTeam SET RSSID=335 WHERE TeamKeyID=354
update SportTeam SET RSSID=336 WHERE TeamKeyID=355
update SportTeam SET RSSID=337 WHERE TeamKeyID=356
update SportTeam SET RSSID=339 WHERE TeamKeyID=360
update SportTeam SET RSSID=340 WHERE TeamKeyID=365
update SportTeam SET RSSID=882 WHERE TeamKeyID=126
update SportTeam SET RSSID=883 WHERE TeamKeyID=132
update SportTeam SET RSSID=884 WHERE TeamKeyID=127
update SportTeam SET RSSID=885 WHERE TeamKeyID=128
update SportTeam SET RSSID=887 WHERE TeamKeyID=135

/* My Teams stuff */
INSERT INTO SportName (SportNameID,RSSID,Name,[Description],LogoURL,Sequence,SeasonStarts,SeasonEnds,[Language],Active,CreationUser,CreationDate,LastUpdateUserTime)
VALUES (29,0,'Amateur Sports','Amateur Sports','',0,GETDATE(),GETDATE(),1,1,'',GETDATE(),'');
INSERT INTO SportName (SportNameID,RSSID,Name,[Description],LogoURL,Sequence,SeasonStarts,SeasonEnds,[Language],Active,CreationUser,CreationDate,LastUpdateUserTime)
VALUES (30,0,'Pro Lacrosse','Professional Lacrosse','',0,GETDATE(),GETDATE(),1,1,'',GETDATE(),'');

Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (68,24,0,'Lacrosse','Youth Sports','',1,'tlayson',GETDATE(),'')

Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (69,29,0,'Baseball','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (70,29,0,'Football','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (71,29,0,'Soccer','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (72,29,0,'Basketball','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (73,29,0,'Hockey','Amateur Sports','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (74,29,0,'Lacrosse','Amateur Sports','',1,'tlayson',GETDATE(),'')

Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (75,30,0,'MLL','Professional Lacrosse','',1,'tlayson',GETDATE(),'')
Insert INTO SportType (SportTypeID,SportNameID,RSSID,Name,[Description],LogoURL,[Language],CreationUser,CreationDate,LastUpdateUserTime)
VALUES (76,30,0,'NLL','Professional Lacrosse','',1,'tlayson',GETDATE(),'')
