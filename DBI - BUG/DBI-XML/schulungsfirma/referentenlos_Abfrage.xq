<referentenlos>
{
for $kv in //kveranst[empty(@referent)]
let $kurs := $kv/..
return 

  <kveranst>
    {($kurs/(@knr, @bezeichn), $kv/(@knrlfnd, @von))}
  </kveranst>
}
</referentenlos>
