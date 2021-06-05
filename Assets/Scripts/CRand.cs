using UnityEngine;

//-----------------------------------------------------------------------
// класс для расчета псевдослучайных последовательностей
// используется линейный конгруэнтный метод генерации 
public class CRand
{
	protected	 ulong m;
	protected	 ulong a;
	protected	 ulong c;
	protected	 ulong x;

	// значения предложенные по умолчанию показывают неплохие характеристики
	// (например величина цикла равна m), но ничего не мешает их переопределить
	// и потестировать полученную последовательность
	public	CRand(uint _x0) { x=_x0; m=2147483647; a=1664525; c=1013904223; }
	public	void Reset(uint _x0) { x=_x0; }
	public	void setM(uint _m) { m=_m; }
	public	void setA(uint _a) { a=_a; }
	public	void setC(uint _c) { c=_c; }
	// собственно формула расчета - если надо использовать значение m
	// отличное от вида 2^n-1 то вместо побитового И надо поставить взятие остатка
	public	uint calc() { return (uint)(x=(((a * x) + c) & m)); }
	// возвращает значение в диапазоне 0.0 до 1.0 НЕ ВКЛЮЧАЯ 1.0
	// (double!) при использовании типа float были существенные потери точности
	public	double get() { return ((double)calc())/(double)(m+1); }
	// возвращает целое значение в диапазоне от 1 до _d как бросок кубика
	public	uint dice(uint _d) { return (uint)(get()*(double)(_d))+1; }
	// устанавливает случайное значение x0
	public void randomize() { Reset( (uint)(Random.value*10000000.0f)); }
};
